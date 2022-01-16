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
        private string tag;

        public int Id { get; set; }
        public int DateForeignKey { get; set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan EndHour { get; private set; }
        public string Tag
        {
            get => tag;
            set
            {
                if (value.Length > 10)
                {
                    tag = value.Substring(0, 10);
                }
                else
                {
                    tag = value;
                }
            }
        }
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

        public DateTime GetStartHourAsDateTime()
        {
            
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, StartHour.Hours, StartHour.Minutes, StartHour.Seconds);
        }

        public DateTime GetEndHourAsDateTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, EndHour.Hours, EndHour.Minutes, EndHour.Seconds);
        }

        public bool CompareStartHour(string startHour)
		{
            var splitDateTime = startHour.Split(" ");
            var splitHour = splitDateTime[1].Split(":");
            var tempStartHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            if(StartHour == tempStartHour)
			{
                return true;
			}
			else
			{
                return false;
			}
        }

        public bool CompareEndHour(string endHour)
        {
            var splitDateTime = endHour.Split(" ");
            var splitHour = splitDateTime[1].Split(":");
            var tempEndHour = new TimeSpan(Convert.ToInt32(splitHour[0]), Convert.ToInt32(splitHour[1]), Convert.ToInt32(splitHour[2]));
            if (StartHour == tempEndHour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
