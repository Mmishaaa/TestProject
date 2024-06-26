﻿// <auto-generated />
using System;
using Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.DepartmentWorker", b =>
                {
                    b.Property<Guid>("DepartmentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DepartmentsId", "WorkersId");

                    b.HasIndex("WorkersId");

                    b.ToTable("DepartmentWorker", (string)null);

                    b.HasData(
                        new
                        {
                            DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            WorkersId = new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2")
                        },
                        new
                        {
                            DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            WorkersId = new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa")
                        },
                        new
                        {
                            DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850")
                        },
                        new
                        {
                            DepartmentsId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            WorkersId = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb")
                        },
                        new
                        {
                            DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            WorkersId = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e")
                        },
                        new
                        {
                            DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            WorkersId = new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb")
                        },
                        new
                        {
                            DepartmentsId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb")
                        },
                        new
                        {
                            DepartmentsId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            WorkersId = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850")
                        });
                });

            modelBuilder.Entity("Entitties.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            Description = "Department for electronics products.",
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            Description = "Department for clothing and apparel.",
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            Description = "Department for books and literature.",
                            Name = "Books"
                        });
                });

            modelBuilder.Entity("Entitties.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fca279a7-ed23-4af6-beed-00c116475ff1"),
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            Description = "Apple iPhone X, 64GB",
                            Name = "iPhone X",
                            Weight = 0.34999999999999998
                        },
                        new
                        {
                            Id = new Guid("be8a93ed-bb52-42a8-a4a1-f1d4d47ff7b9"),
                            DepartmentId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            Description = "Men's denim jeans",
                            Name = "Levi's Jeans",
                            Weight = 1.2
                        },
                        new
                        {
                            Id = new Guid("7ef032cd-05ab-4d01-916e-0c854c6fde76"),
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            Description = "55-inch LED TV",
                            Name = "Samsung TV",
                            Weight = 15.6
                        },
                        new
                        {
                            Id = new Guid("a5c46dc6-37c9-4331-a62f-b5681eceabc8"),
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            Description = "J.K. Rowling's fantasy",
                            Name = "Harry Potter",
                            Weight = 0.90000000000000002
                        },
                        new
                        {
                            Id = new Guid("851a9e39-a386-448e-b023-3c03c53634e9"),
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            Description = "Gaming console",
                            Name = "Sony PlayStation 5",
                            Weight = 4.5
                        },
                        new
                        {
                            Id = new Guid("46671b1f-4e98-4a83-b777-1f69b3b64299"),
                            DepartmentId = new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"),
                            Description = "Men's running shoes",
                            Name = "Nike Air Max",
                            Weight = 0.75
                        },
                        new
                        {
                            Id = new Guid("167c6828-cc07-469a-aa0f-2724c005c89e"),
                            DepartmentId = new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"),
                            Description = "25 cubic feet",
                            Name = "LG Refrigerator",
                            Weight = 85.200000000000003
                        },
                        new
                        {
                            Id = new Guid("50fc8e2b-2e99-4d25-ad3a-0b8d7be1c350"),
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            Description = "Harper Lee's classic novel",
                            Name = "To Kill a Mockingbird",
                            Weight = 0.59999999999999998
                        },
                        new
                        {
                            Id = new Guid("bce83293-a4ff-4742-bdab-9a7431baba7e"),
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            Description = "F. Scott Fitzgerald's masterpiece",
                            Name = "The Great Gatsby",
                            Weight = 0.80000000000000004
                        },
                        new
                        {
                            Id = new Guid("2e7f0bce-bd0e-4aa6-9d43-d322384030ce"),
                            DepartmentId = new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"),
                            Description = "Jane Austen's beloved novel",
                            Name = "Pride and Prejudice",
                            Weight = 0.69999999999999996
                        });
                });

            modelBuilder.Entity("Entitties.Models.Worker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Workers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2"),
                            Age = 35,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa"),
                            Age = 28,
                            FirstName = "Jane",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb"),
                            Age = 35,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e"),
                            Age = 42,
                            FirstName = "David",
                            LastName = "Johnson"
                        },
                        new
                        {
                            Id = new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb"),
                            Age = 31,
                            FirstName = "Sarah",
                            LastName = "Williams"
                        },
                        new
                        {
                            Id = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850"),
                            Age = 40,
                            FirstName = "Michael",
                            LastName = "Anderson"
                        },
                        new
                        {
                            Id = new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984"),
                            Age = 26,
                            FirstName = "Emily",
                            LastName = "Thompson"
                        },
                        new
                        {
                            Id = new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59"),
                            Age = 29,
                            FirstName = "Christopher",
                            LastName = "Rodriguez"
                        },
                        new
                        {
                            Id = new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d"),
                            Age = 33,
                            FirstName = "Olivia",
                            LastName = "Martinez"
                        },
                        new
                        {
                            Id = new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e"),
                            Age = 36,
                            FirstName = "Ethan",
                            LastName = "Davis"
                        });
                });

            modelBuilder.Entity("Entities.Models.DepartmentWorker", b =>
                {
                    b.HasOne("Entitties.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entitties.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Entitties.Models.Product", b =>
                {
                    b.HasOne("Entitties.Models.Department", "Department")
                        .WithMany("Products")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entitties.Models.Department", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
