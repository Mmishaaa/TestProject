using Entitties.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasData
                (
                    new Worker
                    {
                        Id = new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2"),
                        FirstName = "John",
                        LastName = "Doe",
                        Age = 35
                    },
                    new Worker
                    {
                        Id = new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa"),
                        FirstName = "Jane",
                        LastName = "Smith",
                        Age = 28
                    },
                    new Worker
                    {
                        Id = new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb"),
                        FirstName = "John",
                        LastName = "Doe",
                        Age = 35
                    },
                    new Worker
                    {
                        Id = new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e"),
                        FirstName = "David",
                        LastName = "Johnson",
                        Age = 42
                    },
                    new Worker
                    {
                        Id = new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb"),
                        FirstName = "Sarah",
                        LastName = "Williams",
                        Age = 31
                    },
                    new Worker
                    {
                        Id = new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850"),
                        FirstName = "Michael",
                        LastName = "Anderson",
                        Age = 40
                    },
                    new Worker
                    {
                        Id = new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984"),
                        FirstName = "Emily",
                        LastName = "Thompson",
                        Age = 26
                    },
                    new Worker
                    {
                        Id = new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59"),
                        FirstName = "Christopher",
                        LastName = "Rodriguez",
                        Age = 29
                    },
                    new Worker
                    {
                        Id = new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d"),
                        FirstName = "Olivia",
                        LastName = "Martinez",
                        Age = 33
                    },
                    new Worker
                    {
                        Id = new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e"),
                        FirstName = "Ethan",
                        LastName = "Davis",
                        Age = 36
                    }
                );
        }
    }
}
