using Entities.Configuration;
using Entities.Models;
using Entitties.Models;
using Microsoft.EntityFrameworkCore;

namespace Entitties
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());


            modelBuilder.Entity<Department>()
                .HasMany(x => x.Workers)
                .WithMany(y => y.Departments)
                .UsingEntity<DepartmentWorker>(
                    j => j.ToTable("DepartmentWorker")
                        .HasData(
                            new DepartmentWorker { DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), WorkersId = new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2") },
                            new DepartmentWorker { DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), WorkersId = new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa") },
                            new DepartmentWorker { DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") },
                            new DepartmentWorker { DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), WorkersId = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") },
                            new DepartmentWorker { DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), WorkersId = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") },
                            new DepartmentWorker { DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), WorkersId = new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb") },
                            new DepartmentWorker { DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") },
                            new DepartmentWorker { DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") }
                        )
                );

            modelBuilder.Entity<DepartmentWorker>()
                .HasOne(dw => dw.Department)
                .WithMany()
                .HasForeignKey(dw => dw.DepartmentsId);

            modelBuilder.Entity<DepartmentWorker>()
                .HasOne(dw => dw.Worker)
                .WithMany()
                .HasForeignKey(dw => dw.WorkersId);
            

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Worker> Workers { get; set; }

    }
}
