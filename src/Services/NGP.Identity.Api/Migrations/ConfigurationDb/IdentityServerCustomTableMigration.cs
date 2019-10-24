using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGP.Identity.Api.Migrations.ConfigurationDb
{
    public partial class IdentityServerCustomTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysAuth_ApiResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_IdentityResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_IdentityResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ApiClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ApiClaim_SysAuth_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "SysAuth_ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ApiProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ApiProperty_SysAuth_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "SysAuth_ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ApiScope",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ApiScope_SysAuth_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "SysAuth_ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ApiSecret",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ApiSecret_SysAuth_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "SysAuth_ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientClaim_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientCorsOrigin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientCorsOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientCorsOrigin_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientGrantType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrantType = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientGrantType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientGrantType_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientIdpRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientIdpRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientIdpRestriction_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientPostLogoutRedirectUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientPostLogoutRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientPostLogoutRedirectUri_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientProperty_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientRedirectUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientRedirectUri_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientScope",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientScope_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ClientSecret",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ClientSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ClientSecret_SysAuth_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SysAuth_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_IdentityClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_IdentityClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_IdentityClaim_SysAuth_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "SysAuth_IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_IdentityProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_IdentityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_IdentityProperty_SysAuth_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "SysAuth_IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysAuth_ApiScopeClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAuth_ApiScopeClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysAuth_ApiScopeClaim_SysAuth_ApiScope_ApiScopeId",
                        column: x => x.ApiScopeId,
                        principalTable: "SysAuth_ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiClaim_ApiResourceId",
                table: "SysAuth_ApiClaim",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiProperty_ApiResourceId",
                table: "SysAuth_ApiProperty",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiResource_Name",
                table: "SysAuth_ApiResource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiScope_ApiResourceId",
                table: "SysAuth_ApiScope",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiScope_Name",
                table: "SysAuth_ApiScope",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiScopeClaim_ApiScopeId",
                table: "SysAuth_ApiScopeClaim",
                column: "ApiScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ApiSecret_ApiResourceId",
                table: "SysAuth_ApiSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_Client_ClientId",
                table: "SysAuth_Client",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientClaim_ClientId",
                table: "SysAuth_ClientClaim",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientCorsOrigin_ClientId",
                table: "SysAuth_ClientCorsOrigin",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientGrantType_ClientId",
                table: "SysAuth_ClientGrantType",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientIdpRestriction_ClientId",
                table: "SysAuth_ClientIdpRestriction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientPostLogoutRedirectUri_ClientId",
                table: "SysAuth_ClientPostLogoutRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientProperty_ClientId",
                table: "SysAuth_ClientProperty",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientRedirectUri_ClientId",
                table: "SysAuth_ClientRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientScope_ClientId",
                table: "SysAuth_ClientScope",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_ClientSecret_ClientId",
                table: "SysAuth_ClientSecret",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_IdentityClaim_IdentityResourceId",
                table: "SysAuth_IdentityClaim",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_IdentityProperty_IdentityResourceId",
                table: "SysAuth_IdentityProperty",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysAuth_IdentityResource_Name",
                table: "SysAuth_IdentityResource",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysAuth_ApiClaim");

            migrationBuilder.DropTable(
                name: "SysAuth_ApiProperty");

            migrationBuilder.DropTable(
                name: "SysAuth_ApiScopeClaim");

            migrationBuilder.DropTable(
                name: "SysAuth_ApiSecret");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientClaim");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientCorsOrigin");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientGrantType");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientIdpRestriction");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientPostLogoutRedirectUri");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientProperty");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientRedirectUri");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientScope");

            migrationBuilder.DropTable(
                name: "SysAuth_ClientSecret");

            migrationBuilder.DropTable(
                name: "SysAuth_IdentityClaim");

            migrationBuilder.DropTable(
                name: "SysAuth_IdentityProperty");

            migrationBuilder.DropTable(
                name: "SysAuth_ApiScope");

            migrationBuilder.DropTable(
                name: "SysAuth_Client");

            migrationBuilder.DropTable(
                name: "SysAuth_IdentityResource");

            migrationBuilder.DropTable(
                name: "SysAuth_ApiResource");
        }
    }
}
