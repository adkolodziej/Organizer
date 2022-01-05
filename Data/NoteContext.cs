using Microsoft.EntityFrameworkCore;
using Organizer.Models;

namespace Organizer.Data
{
    class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options)
            : base(options)
        {

        }

        public DbSet<Note> Note { get; set; }
    }
}
