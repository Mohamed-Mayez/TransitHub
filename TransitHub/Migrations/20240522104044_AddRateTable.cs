using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitHub.Migrations
{
    public partial class AddRateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OneStar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoStars = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreeStars = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FourStars = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiveStars = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
