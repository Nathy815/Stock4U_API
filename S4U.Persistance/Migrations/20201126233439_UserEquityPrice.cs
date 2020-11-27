using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class UserEquityPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEquityPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    EquityID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquityPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEquityPrices_UserEquities_UserID_EquityID",
                        columns: x => new { x.UserID, x.EquityID },
                        principalTable: "UserEquities",
                        principalColumns: new[] { "UserID", "EquityID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquityPrices_UserID_EquityID",
                table: "UserEquityPrices",
                columns: new[] { "UserID", "EquityID" });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquityPrices_Id_UserID_EquityID",
                table: "UserEquityPrices",
                columns: new[] { "Id", "UserID", "EquityID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEquityPrices");
        }
    }
}
