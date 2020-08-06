using DemoApplication.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext()
        {

        }

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }
        public DbSet<Folder> Folders { get; set; }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DemoDB;");
        }
    }
}
