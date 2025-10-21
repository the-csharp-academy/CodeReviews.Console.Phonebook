using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Migrations
{
    /// <inheritdoc />
    public partial class phonebookdbcreationwithId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneBookDetails",
                table: "PhoneBookDetails");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "PhoneBookDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PhoneBookDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneBookDetails",
                table: "PhoneBookDetails",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneBookDetails",
                table: "PhoneBookDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PhoneBookDetails");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "PhoneBookDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneBookDetails",
                table: "PhoneBookDetails",
                column: "PhoneNumber");
        }
    }
}
