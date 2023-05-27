using LBAPI2.Models;
using Microsoft.EntityFrameworkCore;
namespace LBAPI2.Models
{
    public class LBAPI2Context : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public LBAPI2Context()
        {

        }
        public LBAPI2Context(DbContextOptions<LBAPI2Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
