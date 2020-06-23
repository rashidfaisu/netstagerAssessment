using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    [Table("Person")]
    public class PersonViewModel
    {
        [Key]
        [Column("PersonID")]
        public int PersonID { get; set; }
        [Column("PersonName")]
        public string PersonName { get; set; }
        [Column("PersonAddress")]
        public string PersonAddress { get; set; }
        [NotMapped]
        public string label { get; set; }
    }

    public class Response
    {
        public string type { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        
    }

}
