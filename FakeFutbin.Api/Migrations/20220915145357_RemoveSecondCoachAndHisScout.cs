using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeFutbin.Api.Migrations
{
    public partial class RemoveSecondCoachAndHisScout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scouts",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "CoachName" },
                values: new object[] { 2, "Carlo Ancelotti" });

            migrationBuilder.InsertData(
                table: "Scouts",
                columns: new[] { "Id", "CoachId" },
                values: new object[] { 2, 2 });
        }
    }
}
