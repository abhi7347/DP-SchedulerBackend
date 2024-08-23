using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.DAL.DTOs
{
    public class ProvidersByLocations
    {
        public string DayOfWeek { get; set; }
        public List<int> LocationIds { get; set; }
    }
}
