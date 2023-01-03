using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeFutbin.Api.Migrations
{
    public partial class PlayersPositionupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerNationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerNationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wallet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Raiting = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketValue = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_PlayerNationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "PlayerNationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlayerNationalities",
                columns: new[] { "Id", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, "/Images/Nationalities/Nationality1.jpg", "Brazil" },
                    { 2, "/Images/Nationalities/Nationality2.jpg", "England" },
                    { 3, "/Images/Nationalities/Nationality3.jpg", "France" },
                    { 4, "/Images/Nationalities/Nationality4.jpg", "Italy" },
                    { 5, "/Images/Nationalities/Nationality5.jpg", "Spain" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "ImageURL", "MarketValue", "Name", "NationalityId", "Position", "Qty", "Raiting" },
                values: new object[,]
                {
                    { 1, 30, "/Images/Brazil/Brazil1.jpg", 30000, "Carlos Henrique Casimiro", 1, "CDM,CM", 15, 89 },
                    { 2, 25, "/Images/Brazil/Brazil2.jpg", 35000, "Gabriel Jesus", 1, "ST,RW", 20, 89 },
                    { 3, 34, "/Images/Brazil/Brazil3.jpg", 500000, "Marcelo Vieira da Silva Júnior", 1, "LB,LM", 5, 94 },
                    { 4, 30, "/Images/Brazil/Brazil4.jpg", 264000, "Neymar da Silva Santos Jr.", 1, "LW,CAM", 7, 96 },
                    { 5, 22, "/Images/Brazil/Brazil5.jpg", 221000, "Vinícius José de Oliveira Júnior", 1, "LW,RW", 13, 96 },
                    { 6, 42, "/Images/England/England1.jpg", 215000, "Steven Gerrard", 2, "CM,CDM", 4, 92 },
                    { 7, 44, "/Images/England/England2.jpg", 132000, "Frank Lampard", 2, "CM,CAM", 6, 91 },
                    { 8, 36, "/Images/England/England3.jpg", 349000, "Wayne Rooney", 2, "ST,CF", 8, 93 },
                    { 9, 22, "/Images/England/England4.jpg", 110000, "Jadon Sancho", 2, "RM,RW", 20, 91 },
                    { 10, 47, "/Images/England/England5.jpg", 117000, "Paul Scholes", 2, "CM,CAM", 6, 92 },
                    { 11, 34, "/Images/France/France1.jpg", 230000, "Karim Benzema", 3, "CF,ST", 9, 98 },
                    { 12, 56, "/Images/France/France2.jpg", 390000, "Eric Cantona", 3, "CF,ST", 5, 94 },
                    { 13, 45, "/Images/France/France3.jpg", 562000, "Thierry Henry", 3, "ST,LW", 8, 94 },
                    { 14, 49, "/Images/France/France4.jpg", 110000, "Claude Makélélé", 3, "CM,CDM", 10, 91 },
                    { 15, 50, "/Images/France/France5.jpg", 1581000, "Zinedine Zidane", 3, "CAM,CM", 3, 97 },
                    { 16, 48, "/Images/Italy/Italy1.jpg", 195000, "Fabio Cannavaro", 4, "CB,RB", 6, 93 },
                    { 17, 38, "/Images/Italy/Italy2.jpg", 137000, "Giorgio Chiellini", 4, "CB,LB", 11, 96 },
                    { 18, 47, "/Images/Italy/Italy3.jpg", 155000, "Alessandro Del Piero", 4, "CF,ST", 13, 93 },
                    { 19, 44, "/Images/Italy/Italy4.jpg", 100000, "Gennaro Gattuso", 4, "CDM,CM", 15, 90 },
                    { 20, 43, "/Images/Italy/Italy5.jpg", 179000, "Andrea Pirlo", 4, "CM,CAM", 12, 93 },
                    { 21, 41, "/Images/Spain/Spain1.jpg", 339000, "Iker Casillas", 5, "GK", 5, 93 },
                    { 22, 38, "/Images/Spain/Spain2.jpg", 10000, "Andrés Iniesta Luján", 5, "CM,CAM", 13, 83 },
                    { 23, 36, "/Images/Spain/Spain3.jpg", 8500, "Sergio Ramos García", 5, "CB,RB", 17, 88 },
                    { 24, 45, "/Images/Spain/Spain4.jpg", 105000, "Raúl González Blanco", 5, "CF,ST", 5, 93 },
                    { 25, 38, "/Images/Spain/Spain5.jpg", 156000, "Fernando Torres", 5, "ST,CF", 9, 92 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_NationalityId",
                table: "Players",
                column: "NationalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "UserPlayers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlayerNationalities");
        }
    }
}
