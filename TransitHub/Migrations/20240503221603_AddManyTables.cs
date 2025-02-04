using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitHub.Migrations
{
    public partial class AddManyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_AspNetUsers_ApplicationUserId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_ApplicationUserId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Trip");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Trip",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LocalTransports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VichelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalTransports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalTransports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shipps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TripId = table.Column<int>(type: "int", nullable: true),
                    LocalTransportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipps_LocalTransports_LocalTransportId",
                        column: x => x.LocalTransportId,
                        principalTable: "LocalTransports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipps_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_userId",
                table: "Trip",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalTransports_UserId",
                table: "LocalTransports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipps_LocalTransportId",
                table: "Shipps",
                column: "LocalTransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipps_TripId",
                table: "Shipps",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_AspNetUsers_userId",
                table: "Trip",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_AspNetUsers_userId",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "Shipps");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "LocalTransports");

            migrationBuilder.DropIndex(
                name: "IX_Trip_userId",
                table: "Trip");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Trip",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ApplicationUserId",
                table: "Trip",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_AspNetUsers_ApplicationUserId",
                table: "Trip",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
