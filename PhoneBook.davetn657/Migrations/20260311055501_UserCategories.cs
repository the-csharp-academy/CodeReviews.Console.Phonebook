using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.davetn657.Migrations
{
    /// <inheritdoc />
    public partial class UserCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PhoneProperties",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "PhoneProperties");
        }
    }
}
