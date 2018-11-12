using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPackUpdater.Migrations
{
    public partial class migration20181112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangedOn",
                table: "WebResourceMaps",
                newName: "CreatedOn");

            migrationBuilder.CreateTable(
                name: "ChangedWebResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: false),
                    WebResourceMapId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangedWebResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangedWebResources_WebResourceMaps_WebResourceMapId",
                        column: x => x.WebResourceMapId,
                        principalTable: "WebResourceMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangedWebResources_WebResourceMapId",
                table: "ChangedWebResources",
                column: "WebResourceMapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangedWebResources");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "WebResourceMaps",
                newName: "ChangedOn");
        }
    }
}
