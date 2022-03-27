#nullable disable

namespace MobileBG.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class KmColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Km",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Km",
                table: "Cars");
        }
    }
}
