using Entitties.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
                (
                    new Department
                    {
                        Id = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                        Name = "Electronics",
                        Description = "Department for electronics products.",
                    },
                    new Department
                    {
                        Id = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                        Name = "Clothing",
                        Description = "Department for clothing and apparel.",
                    },
                    new Department
                    {
                        Id = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                        Name = "Books",
                        Description = "Department for books and literature.",
                    }
                );
        }
    }
}
