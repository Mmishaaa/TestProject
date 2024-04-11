using Entitties.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasData
                    (
                        new Product
                        {
                            Id = new Guid("fca279a7-ed23-4af6-beed-00c116475ff1"),
                            Name = "iPhone X",
                            Description = "Apple iPhone X, 64GB",
                            Weight = 0.35,
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3")
                        },
                        new Product
                        {
                            Id = new Guid("be8a93ed-bb52-42a8-a4a1-f1d4d47ff7b9"),
                            Name = "Levi's Jeans",
                            Description = "Men's denim jeans",
                            Weight = 1.2,
                            DepartmentId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932")
                        },
                        new Product
                        {
                            Id = new Guid("7ef032cd-05ab-4d01-916e-0c854c6fde76"),
                            Name = "Samsung TV",
                            Description = "55-inch LED TV",
                            Weight = 15.6,
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3")
                        },
                        new Product
                        {
                            Id = new Guid("a5c46dc6-37c9-4331-a62f-b5681eceabc8"),
                            Name = "Harry Potter",
                            Description = "J.K. Rowling's fantasy",
                            Weight = 0.9,
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95")
                        },
                        new Product
                        {
                            Id = new Guid("851a9e39-a386-448e-b023-3c03c53634e9"),
                            Name = "Sony PlayStation 5",
                            Description = "Gaming console",
                            Weight = 4.5,
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3")
                        },
                        new Product
                        {
                            Id = new Guid("46671b1f-4e98-4a83-b777-1f69b3b64299"),
                            Name = "Nike Air Max",
                            Description = "Men's running shoes",
                            Weight = 0.75,
                            DepartmentId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932")
                        },
                        new Product
                        {
                            Id = new Guid("167c6828-cc07-469a-aa0f-2724c005c89e"),
                            Name = "LG Refrigerator",
                            Description = "25 cubic feet",
                            Weight = 85.2,
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3")
                        },
                        new Product
                         {
                            Id = new Guid("50fc8e2b-2e99-4d25-ad3a-0b8d7be1c350"),
                            Name = "To Kill a Mockingbird",
                            Description = "Harper Lee's classic novel",
                            Weight = 0.6,
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95")
                        },
                         new Product
                         {
                             Id = new Guid("bce83293-a4ff-4742-bdab-9a7431baba7e"),
                             Name = "The Great Gatsby",
                             Description = "F. Scott Fitzgerald's masterpiece",
                             Weight = 0.8,
                             DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95")
                         },
                         new Product
                         {
                             Id = new Guid("2e7f0bce-bd0e-4aa6-9d43-d322384030ce"),
                             Name = "Pride and Prejudice",
                             Description = "Jane Austen's beloved novel",
                             Weight = 0.7,
                             DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95")
                         }
                    ); ;
        }
    }
}
