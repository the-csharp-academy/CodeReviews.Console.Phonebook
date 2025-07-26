using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.GoldRino456.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndCategoryList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactCategories_CategoryId",
                table: "Contacts",
                column: "CategoryId",
                principalTable: "ContactCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactCategories_CategoryId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
