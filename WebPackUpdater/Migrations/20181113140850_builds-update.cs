using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPackUpdater.Migrations
{
    public partial class buildsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildStatusType",
                table: "Builds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Builds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildStatusType",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Builds");
        }
    }
}
