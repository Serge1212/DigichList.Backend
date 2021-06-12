using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class whenuserdeletedpublisherissettonullindefects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Users_UserId",
                table: "Defects");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Users_UserId",
                table: "Defects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Users_UserId",
                table: "Defects");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Users_UserId",
                table: "Defects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
