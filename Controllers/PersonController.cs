using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using test.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace test.Controllers
{
    public class PersonController : Controller
    {
        private DataContext db = new DataContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Search(string PersonName)
        {            
            var Person = new List<PersonViewModel>();
            Person = db.Person.Where(p => p.PersonName.ToLower().Contains(PersonName.ToLower())).ToList();
            Person.ForEach(a => a.label = a.PersonName);
            return JsonConvert.SerializeObject(Person);
        }

        [HttpPost]
        public Response SavePerson(PersonViewModel objPersonViewModel)
        {
            Response objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Person.Add(objPersonViewModel);
                    db.SaveChanges();
                    objResponse.title = "Success";
                    objResponse.message = "Insert Success";
                }
                else
                {
                    objResponse.title = "Failed";
                    objResponse.message = "Insert Failed. Check The fields are correct";
                }
            }
            catch (Exception ex)
            {
                objResponse.title = "Failed";
                objResponse.message = ex.Message;
            }
            return objResponse;
        }

        [HttpPost]
        public IRestResponse SendEmail(string ToEmail,string Name,string Address)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "6d90282cd737563851034175d022eb8d-1b6eb03d-7b186e51");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "https://localhost:44393/", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mohammed Rashid <rashidfaisu@gmail.com>");
            request.AddParameter("to", "mohammedrashidmadathil@gmail.com");
            request.AddParameter("to", "mohammedrashidmadathil@gmail.com");
            request.AddParameter("subject", "User Details");
            request.AddParameter("text", "Name:"+Name+"    Address:"+Address+"");
            request.Method = Method.POST;
            var data=client.Execute(request);
            return data;
        }
    }
}