using Microsoft.EntityFrameworkCore;
using ToDoodle.Data.Model;

namespace ToDoodle.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
