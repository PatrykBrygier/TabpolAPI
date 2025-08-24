using Microsoft.EntityFrameworkCore;
using Tabpol.Entities;

namespace Tabpol.Data
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
