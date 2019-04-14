using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appLab.Models
{
    public class PageViewModel
    {
        public int PageCount { get; set; }
        public List<Event> Items { get; set; }
    }
}
