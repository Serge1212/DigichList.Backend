using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class RemovedTechniciantable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDefects_Users_TechnicianId",
                table: "AssignedDefects");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "AssignedDefects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedDefects_TechnicianId",
                table: "AssignedDefects",
                newName: "IX_AssignedDefects_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDefects_Users_UserId",
                table: "AssignedDefects");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AssignedDefects",
                newName: "TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedDefects_UserId",
                table: "AssignedDefects",
                newName: "IX_AssignedDefects_TechnicianId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDefects_Users_TechnicianId",
                table: "AssignedDefects",
                column: "TechnicianId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
