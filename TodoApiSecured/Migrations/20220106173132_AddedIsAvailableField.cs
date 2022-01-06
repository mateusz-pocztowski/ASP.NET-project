using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class AddedIsAvailableField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "BooksItems",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "BooksItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "BooksItems");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "BooksItems",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }
    }
}
