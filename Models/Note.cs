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
        public DateTime StartHour { get; private set; }
        public DateTime EndHour { get; private set; }
        public string Content { get; set; }

        public void SetStartHour(string time)
        {
            StartHour = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public void SetEndHour(string time)
        {
            EndHour = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
