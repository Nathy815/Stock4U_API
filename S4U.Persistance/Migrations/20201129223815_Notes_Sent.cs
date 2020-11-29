using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class Notes_Sent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "Notes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Notes");
        }
    }
}
