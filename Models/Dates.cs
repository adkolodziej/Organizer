using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class Dates
    {
        private List<Date> dates;

        public List<Date> ListOfDates { get => dates; }

        public Dates(List<Date> dates)
        {
            this.dates = dates;
        }
    }
}
