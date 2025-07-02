using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Phone_Book.Migrations
{
    /// <inheritdoc />
    public partial class updatefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "jhusher0@ted.com", "Julienne Husher", "128-187-6389" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 1, "fkemball1@webmd.com", "Farlay Kemball", "178-994-6729" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 1, "ggarahan2@51.la", "Golda Garahan", "468-314-0933" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 2, "lmaccague3@w3.org", "Lisha MacCague", "999-787-9493" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "nbockin4@taobao.com", "Nikki Bockin", "847-451-1638" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 2, "jgrishanov5@blogger.com", "Jemimah Grishanov", "606-400-0266" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 7, 3, "tlettuce6@tuttocitta.it", "Tersina Lettuce", "164-204-3869" },
                    { 8, 3, "grampling7@yellowpages.com", "Gradey Rampling", "649-743-0045" },
                    { 9, 3, "smollnar8@hubpages.com", "Simonette Mollnar", "120-226-6327" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "JDoe@gmail.com", "John Doe", "123-456-7890" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 2, "jsmith@hotmail.com", "Jane Smith", "098-765-4321" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 3, "alicej@gmail.com", "Alice Johnson", "555-555-5555" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 1, "bbrown@yahoo.com", "Bob Brown", "444-444-4444" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "charliew@aol.com", "Charlie White", "333-333-3333" });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Email", "Name", "Phone" },
                values: new object[] { 3, "davidg@gmail.com", "David Green", "222-222-2222" });
        }
    }
}
