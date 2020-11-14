using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class Equity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Email",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Equities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Ticker = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEquities",
                columns: table => new
                {
                    UserID = table.Column<Guid>(nullable: false),
                    EquityID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquities", x => new { x.UserID, x.EquityID });
                    table.ForeignKey(
                        name: "FK_UserEquities_Equities_EquityID",
                        column: x => x.EquityID,
                        principalTable: "Equities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEquities_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Email",
                table: "Users",
                columns: new[] { "Id", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equities_Id_Ticker",
                table: "Equities",
                columns: new[] { "Id", "Ticker" });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquities_EquityID",
                table: "UserEquities",
                column: "EquityID");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquities_UserID_EquityID",
                table: "UserEquities",
                columns: new[] { "UserID", "EquityID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEquities");

            migrationBuilder.DropTable(
                name: "Equities");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Email",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Email",
                table: "Users",
                columns: new[] { "Id", "Email" });
        }
    }
}
