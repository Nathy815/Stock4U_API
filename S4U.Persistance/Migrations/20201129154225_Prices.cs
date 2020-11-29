using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class Prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "UserEquityPrices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sent",
                table: "UserEquityPrices");
        }
    }
}
