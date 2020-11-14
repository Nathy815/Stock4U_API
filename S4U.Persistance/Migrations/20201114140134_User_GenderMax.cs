using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class User_GenderMax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Address",
                newName: "Neighborhood");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 9,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "Address",
                newName: "Country");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);
        }
    }
}
