using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class UserEquity_DeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompareEquities_UserEquities_UserID_EquityID",
                table: "CompareEquities");

            migrationBuilder.AddForeignKey(
                name: "FK_CompareEquities_UserEquities_UserID_EquityID",
                table: "CompareEquities",
                columns: new[] { "UserID", "EquityID" },
                principalTable: "UserEquities",
                principalColumns: new[] { "UserID", "EquityID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompareEquities_UserEquities_UserID_EquityID",
                table: "CompareEquities");

            migrationBuilder.AddForeignKey(
                name: "FK_CompareEquities_UserEquities_UserID_EquityID",
                table: "CompareEquities",
                columns: new[] { "UserID", "EquityID" },
                principalTable: "UserEquities",
                principalColumns: new[] { "UserID", "EquityID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
