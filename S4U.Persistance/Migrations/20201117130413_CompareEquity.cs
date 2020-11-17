using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class CompareEquity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompareEquities",
                columns: table => new
                {
                    UserID = table.Column<Guid>(nullable: false),
                    EquityID = table.Column<Guid>(nullable: false),
                    CompareID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompareEquities", x => new { x.UserID, x.EquityID, x.CompareID });
                    table.UniqueConstraint("AK_CompareEquities_CompareID_UserID", x => new { x.CompareID, x.UserID });
                    table.ForeignKey(
                        name: "FK_CompareEquities_Equities_CompareID",
                        column: x => x.CompareID,
                        principalTable: "Equities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompareEquities_UserEquities_UserID_EquityID",
                        columns: x => new { x.UserID, x.EquityID },
                        principalTable: "UserEquities",
                        principalColumns: new[] { "UserID", "EquityID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompareEquities_UserID_EquityID_CompareID",
                table: "CompareEquities",
                columns: new[] { "UserID", "EquityID", "CompareID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompareEquities");
        }
    }
}
