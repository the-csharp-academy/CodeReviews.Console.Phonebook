using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.davetn657.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PhoneProperties");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PhoneProperties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PhoneProperties",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PhoneProperties",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
