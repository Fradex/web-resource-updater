using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPackUpdater.Migrations
{
    public partial class builds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BuildId",
                table: "ChangedWebResources",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BuildName = table.Column<string>(nullable: true),
                    BuildDescription = table.Column<string>(nullable: true),
                    BuildLog = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangedWebResources_BuildId",
                table: "ChangedWebResources",
                column: "BuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangedWebResources_Builds_BuildId",
                table: "ChangedWebResources",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangedWebResources_Builds_BuildId",
                table: "ChangedWebResources");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_ChangedWebResources_BuildId",
                table: "ChangedWebResources");

            migrationBuilder.DropColumn(
                name: "BuildId",
                table: "ChangedWebResources");
        }
    }
}
