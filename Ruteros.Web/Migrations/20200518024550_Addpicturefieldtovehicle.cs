using Microsoft.EntityFrameworkCore.Migrations;

namespace Ruteros.Web.Migrations
{
    public partial class Addpicturefieldtovehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Vehicles");
        }
    }
}
