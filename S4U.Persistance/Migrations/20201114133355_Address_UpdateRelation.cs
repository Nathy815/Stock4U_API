using Microsoft.EntityFrameworkCore.Migrations;

namespace S4U.Persistance.Migrations
{
    public partial class Address_UpdateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_Id",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_Id_ZipCode",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Id_ZipCode",
                table: "Address",
                columns: new[] { "Id", "ZipCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Address_Id_ZipCode",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Id_ZipCode",
                table: "Address",
                columns: new[] { "Id", "ZipCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_Id",
                table: "Address",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
