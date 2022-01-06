
using System.Collections.Generic;

namespace Organizer.Models
{
    public class Date
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<Note> Notes {get;set;}
    }
}
