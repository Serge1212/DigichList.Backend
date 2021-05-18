using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class addedCanFixDefectscolumntoRolestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanFixDefects",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanFixDefects",
                table: "Roles");
        }
    }
}
