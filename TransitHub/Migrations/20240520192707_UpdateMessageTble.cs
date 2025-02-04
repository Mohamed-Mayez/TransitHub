using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitHub.Migrations
{
    public partial class UpdateMessageTble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWrite",
                table: "Messages",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Messages",
                newName: "IsWrite");
        }
    }
}
