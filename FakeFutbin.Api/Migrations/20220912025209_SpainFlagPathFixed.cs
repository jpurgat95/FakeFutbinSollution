using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeFutbin.Api.Migrations
{
    public partial class SpainFlagPathFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PlayerNationalities",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageURL",
                value: "/Images/Nationalities/Nationality5.jpg");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NationalityId",
                table: "Players",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerNationalities_NationalityId",
                table: "Players",
                column: "NationalityId",
                principalTable: "PlayerNationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerNationalities_NationalityId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_NationalityId",
                table: "Players");

            migrationBuilder.UpdateData(
                table: "PlayerNationalities",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageURL",
                value: "/Images/Nationalities/Nationality4.jpg");
        }
    }
}
