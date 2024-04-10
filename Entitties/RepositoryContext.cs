using Entitties.Models;
using Microsoft.EntityFrameworkCore;

namespace Entitties
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
