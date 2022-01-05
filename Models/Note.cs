using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int DateForeignKey { get; set; }
        public int Hour { get; set; }
        public string Content { get; set; }
    }
}
