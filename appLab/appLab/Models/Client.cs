using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appLab.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Patronymic { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string IdendificationNumber { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
