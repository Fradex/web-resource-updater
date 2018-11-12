using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPackUpdater.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebResourceMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChangedOn = table.Column<DateTime>(nullable: false),
                    CrmWebResourceId = table.Column<Guid>(nullable: false),
                    FileSystemPath = table.Column<string>(nullable: true),
                    CrmFileName = table.Column<string>(nullable: true),
                    LocalFileMd5Hash = table.Column<string>(nullable: true),
                    IsAutoUpdate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebResourceMaps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebResourceMaps");
        }
    }
}
