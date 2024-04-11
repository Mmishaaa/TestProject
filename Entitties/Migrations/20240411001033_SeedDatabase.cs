using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), "Department for books and literature.", "Books" },
                    { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), "Department for electronics products.", "Electronics" },
                    { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), "Department for clothing and apparel.", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Age", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850"), 40, "Michael", "Anderson" },
                    { new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59"), 29, "Christopher", "Rodriguez" },
                    { new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e"), 42, "David", "Johnson" },
                    { new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb"), 31, "Sarah", "Williams" },
                    { new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa"), 28, "Jane", "Smith" },
                    { new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb"), 35, "John", "Doe" },
                    { new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2"), 35, "John", "Doe" },
                    { new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d"), 33, "Olivia", "Martinez" },
                    { new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e"), 36, "Ethan", "Davis" },
                    { new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984"), 26, "Emily", "Thompson" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentWorker",
                columns: new[] { "DepartmentsId", "WorkersId" },
                values: new object[,]
                {
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e") },
                    { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984") },
                    { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") },
                    { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa") },
                    { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") },
                    { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2") },
                    { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") },
                    { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") },
                    { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DepartmentId", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("167c6828-cc07-469a-aa0f-2724c005c89e"), new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), "25 cubic feet", "LG Refrigerator", 85.200000000000003 },
                    { new Guid("2e7f0bce-bd0e-4aa6-9d43-d322384030ce"), new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), "Jane Austen's beloved novel", "Pride and Prejudice", 0.69999999999999996 },
                    { new Guid("46671b1f-4e98-4a83-b777-1f69b3b64299"), new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), "Men's running shoes", "Nike Air Max", 0.75 },
                    { new Guid("50fc8e2b-2e99-4d25-ad3a-0b8d7be1c350"), new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), "Harper Lee's classic novel", "To Kill a Mockingbird", 0.59999999999999998 },
                    { new Guid("7ef032cd-05ab-4d01-916e-0c854c6fde76"), new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), "55-inch LED TV", "Samsung TV", 15.6 },
                    { new Guid("851a9e39-a386-448e-b023-3c03c53634e9"), new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), "Gaming console", "Sony PlayStation 5", 4.5 },
                    { new Guid("a5c46dc6-37c9-4331-a62f-b5681eceabc8"), new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), "J.K. Rowling's fantasy", "Harry Potter", 0.90000000000000002 },
                    { new Guid("bce83293-a4ff-4742-bdab-9a7431baba7e"), new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), "F. Scott Fitzgerald's masterpiece", "The Great Gatsby", 0.80000000000000004 },
                    { new Guid("be8a93ed-bb52-42a8-a4a1-f1d4d47ff7b9"), new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), "Men's denim jeans", "Levi's Jeans", 1.2 },
                    { new Guid("fca279a7-ed23-4af6-beed-00c116475ff1"), new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), "Apple iPhone X, 64GB", "iPhone X", 0.34999999999999998 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"), new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"), new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e") });

            migrationBuilder.DeleteData(
                table: "DepartmentWorker",
                keyColumns: new[] { "DepartmentsId", "WorkersId" },
                keyValues: new object[] { new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"), new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("167c6828-cc07-469a-aa0f-2724c005c89e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2e7f0bce-bd0e-4aa6-9d43-d322384030ce"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("46671b1f-4e98-4a83-b777-1f69b3b64299"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50fc8e2b-2e99-4d25-ad3a-0b8d7be1c350"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7ef032cd-05ab-4d01-916e-0c854c6fde76"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("851a9e39-a386-448e-b023-3c03c53634e9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a5c46dc6-37c9-4331-a62f-b5681eceabc8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bce83293-a4ff-4742-bdab-9a7431baba7e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("be8a93ed-bb52-42a8-a4a1-f1d4d47ff7b9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fca279a7-ed23-4af6-beed-00c116475ff1"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("536d8b1a-bb2a-4e22-8258-bcebe06a2f95"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("913a0fdb-359c-4eae-a0e8-227c7fd920c3"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("cea35bab-e8ea-4a79-b47d-4ec00c53e932"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("3a266ee0-f9bc-4738-9960-b00f583c8850"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("631fd318-e5f8-4bcd-bfcc-07bef265dd59"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("7adb2fbd-9d09-4fc5-9b5d-097a07fc0a9e"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("84d0c25d-4ec8-41be-a6b0-47801abfcceb"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("ad11d22c-e5db-471a-9693-b1b0716f98aa"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("c13cb7e9-4adf-4c71-baba-fa107de50ccb"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("c30d6b8e-0917-4186-a9af-3cab2e297ff2"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("c74ce701-ca4c-47b1-b7b2-2e19c91ca69d"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("d358f48f-ba6c-451f-beaa-0257f7be274e"));

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: new Guid("f37b3a8b-9c18-4055-8a19-e4ddbbb72984"));
        }
    }
}
