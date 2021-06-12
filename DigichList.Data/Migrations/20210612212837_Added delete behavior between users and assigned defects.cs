using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class Addeddeletebehaviorbetweenusersandassigneddefects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
