using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appLab.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Column(TypeName  = "nvarchar(50)")]
        public string NameEvent { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string DesctiptionEvent { get; set; }
        [DataType(DataType.Date)]
        public DateTime DurationDateEvent { get; set; }
        [DataType(DataType.Time)]
        public DateTime DurationTimeEvent { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
