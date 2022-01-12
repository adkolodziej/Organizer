
using System.Collections.Generic;

namespace Organizer.Models
{
    public class Date
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<Note> Notes { get; set; }

        public bool CompareDates(Date date)
        {
            if (Id != 0 && date.Id != 0)
            {
                if (Id != date.Id)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (Year == date.Year && Month == date.Month && Day == date.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetNotesHours()
        {
            List<string> hours = new List<string>();
            foreach (var note in Notes)
            {
                hours.Add(note.StartHour.ToString() + "-" + note.EndHour.ToString());
            }
            return hours;
        }
    }
}
