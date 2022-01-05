using Microsoft.EntityFrameworkCore;
using Organizer.Models;

namespace Organizer.Data
{
    public class DateContext: DbContext
    {
        public DateContext(DbContextOptions<DateContext> options)
            :base(options)
        {

        }

        public DbSet<Date> Date { get; set; }
    }
}
