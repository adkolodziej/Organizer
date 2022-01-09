using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int DateForeignKey { get; set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan EndHour { get; private set; }
        public string Content { get; set; }

        public void SetStartHour(string time, bool isFromDB = false)
        {
            if (isFromDB == false)
            {
                var splitDateTime = time.Split(" ");
                var splitHour = splitDateTime[1].Split(":");
                StartHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            }
            else
            {
                var splitHour = time.Split(":");
                StartHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            }

        }

        public void SetEndHour(string time, bool isFromDB = false)
        {
            if (isFromDB == false)
            {
                var splitDateTime = time.Split(" ");
                var splitHour = splitDateTime[1].Split(":");
                EndHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            }
            else
            {
                var splitHour = time.Split(":");
                EndHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            }
        }
    }
}
