using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class RemovedWantsToRegistercolumnfromUserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WantsToRegister",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WantsToRegister",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
