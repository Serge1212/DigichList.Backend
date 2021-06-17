using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class addednewfieldstoAdminandremoveusernamefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Admins",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Admins",
                newName: "Username");
        }
    }
}
