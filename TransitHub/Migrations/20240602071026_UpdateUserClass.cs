using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitHub.Migrations
{
    public partial class UpdateUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPassword",
                table: "AspNetUsers");
        }
    }
}
