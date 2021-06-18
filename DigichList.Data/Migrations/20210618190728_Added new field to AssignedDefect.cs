using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigichList.Infrastructure.Migrations
{
    public partial class AddednewfieldtoAssignedDefect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "AssignedDefects",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "AssignedDefects");
        }
    }
}
