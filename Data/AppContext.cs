using Microsoft.EntityFrameworkCore;
using UserProfile.Models;

namespace UserProfile.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
