using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PhoneBook.Data;

#nullable disable

namespace PhoneBook.Migrations;

[DbContext(typeof(PhoneBookContext))]
[Migration("20260710000000_AddUniqueCategoryName")]
public partial class AddUniqueCategoryName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Categories_Name",
            table: "Categories");
    }
}
