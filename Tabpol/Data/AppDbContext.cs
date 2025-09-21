using Microsoft.EntityFrameworkCore;
using Tabpol.Entities;

namespace Tabpol.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<TabType> TabTypes { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Excersise> Excersises { get; set; }
        public DbSet<PracticeBlock> PracticeBlocks { get; set; }
    }
}
