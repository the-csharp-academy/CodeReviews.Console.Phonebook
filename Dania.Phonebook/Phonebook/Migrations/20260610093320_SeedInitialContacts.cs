using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Phonebook.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "johnDoe@gmail.com", "John Doe", "110000111" },
                    { 2, "john.doe@gmail.com", "John Doe", "114345111" },
                    { 3, "maryjane@gmail.com", "Mary Jane", "110500631" },
                    { 4, "alicegreen@gmail.com", "Alice Green", "120305141" },
                    { 5, "bobvance@gmail.com", "Bob Vance", "120775991" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
