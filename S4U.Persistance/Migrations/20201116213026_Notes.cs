using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class Notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserEquities_EquityID",
                table: "UserEquities");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserEquities_EquityID_UserID",
                table: "UserEquities",
                columns: new[] { "EquityID", "UserID" });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Comments = table.Column<string>(maxLength: 256, nullable: true),
                    Attach = table.Column<string>(maxLength: 256, nullable: true),
                    Alert = table.Column<DateTime>(nullable: true),
                    EquityID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_UserEquities_UserID_EquityID",
                        columns: x => new { x.UserID, x.EquityID },
                        principalTable: "UserEquities",
                        principalColumns: new[] { "UserID", "EquityID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserID_EquityID",
                table: "Notes",
                columns: new[] { "UserID", "EquityID" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id_UserID_EquityID_Alert",
                table: "Notes",
                columns: new[] { "Id", "UserID", "EquityID", "Alert" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserEquities_EquityID_UserID",
                table: "UserEquities");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquities_EquityID",
                table: "UserEquities",
                column: "EquityID");
        }
    }
}
