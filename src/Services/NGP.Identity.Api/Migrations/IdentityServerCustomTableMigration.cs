using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGP.Identity.Api.Migrations
{
    public partial class IdentityServerCustomTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysAuth_DeviceCode",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_DeviceCode", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_PersistedGrant",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_PersistedGrant", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_DeviceCode_DeviceCode",
                table: "SysAuth_DeviceCode",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_DeviceCode_Expiration",
                table: "SysAuth_DeviceCode",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_PersistedGrant_SubjectId_ClientId_Type_Expiration",
                table: "SysAuth_PersistedGrant",
                columns: new[] { "SubjectId", "ClientId", "Type", "Expiration" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysAuth_DeviceCode");

            migrationBuilder.DropTable(
                name: "SysAuth_PersistedGrant");
        }
    }
}
