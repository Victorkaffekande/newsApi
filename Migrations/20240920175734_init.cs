using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace newsApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40d75bab-de0d-480f-9077-f7c672b2b9df", "40d75bab-de0d-480f-9077-f7c672b2b9df", "Writer", "WRITER" },
                    { "a053fdb1-2e00-4cc9-996e-d5a7f5a244fa", "a053fdb1-2e00-4cc9-996e-d5a7f5a244fa", "Editor", "EDITOR" },
                    { "d1ec52b1-6223-42f6-85b7-8af22edb439c", "d1ec52b1-6223-42f6-85b7-8af22edb439c", "Subscriber", "SUBSCRIBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1927b008-f316-4535-9328-958997f88a39", 0, "421bc48d-9f74-4ec2-bb86-94b17404354f", "otherWriter@mail.com", false, false, null, "OTHERWRITER@MAIL.COM", "OTHERWRITER@MAIL.COM", "AQAAAAIAAYagAAAAEJeE8bl7BqLP1QN5cd+ZykFQqN7q2HFNkGOZNuERcCwgMzP+UWbqSo+qljfXAEEVOw==", null, false, "152dc4cc-55c0-4577-a019-f7afab040578", false, "otherWriter@mail.com" },
                    { "57a0fb60-4421-46d1-9931-9a54b3347d85", 0, "6e3b4357-7217-4fdb-9b59-4304c0b671dc", "writer@mail.com", false, false, null, "WRITER@MAIL.COM", "WRITER@MAIL.COM", "AQAAAAIAAYagAAAAEFqeOoJiKEyMRSJ9qqrMpCtb/eFWIWb6C7U9xxzGydn81BHwmItOXFIg5jUMfEx+Jg==", null, false, "56fb9af1-8f29-4520-a733-a55008878aea", false, "writer@mail.com" },
                    { "69e928e3-4b49-42e2-a6bb-3724400398ab", 0, "af6b8aca-87a5-4f29-a042-2f8e9614c704", "otherSubscriber@mail.com", false, false, null, "OTHERSUBSCRIBER@MAIL.COM", "OTHERSUBSCRIBER@MAIL.COM", "AQAAAAIAAYagAAAAEIFdLrdBt+pZle2Y0505RUyn0LTbaiu+tqpUnEWBIWz4AirV/64FtPD6cywI+7u47g==", null, false, "f19d8b6b-3cec-4fb6-83f9-f8e2e7d946b1", false, "otherSubscriber@mail.com" },
                    { "e55487c1-37f7-4cad-88f1-13c089ceb2c6", 0, "96b2ad96-8389-4f88-9745-9dcc41751ed6", "subscriber@mail.com", false, false, null, "SUBSCRIBER@MAIL.COM", "SUBSCRIBER@MAIL.COM", "AQAAAAIAAYagAAAAEE8OJFw9SzOFqFubelqjUKNwjiegFEVgniPnQpoQNLGU6lpTQqGiYb8SROqYVPbT6g==", null, false, "0496a4ad-1e9c-4a7a-8c2b-67059b7bdc42", false, "subscriber@mail.com" },
                    { "faa054ae-0eb7-44db-b34f-10bc5fc9fe09", 0, "703f8e9f-c13f-47ff-8033-a1f3f22b83ee", "editor@mail.com", false, false, null, "EDITOR@MAIL.COM", "EDITOR@MAIL.COM", "AQAAAAIAAYagAAAAEPYNQhjryQ3YieJIUXmse5psMvnc31Eq/AufaB/ThLObQo8NNUTC6mX6XgrRpVvT/w==", null, false, "df8a0aa7-6885-4e80-b146-1c40f0937e4c", false, "editor@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "Title" },
                values: new object[,]
                {
                    { 1, "57a0fb60-4421-46d1-9931-9a54b3347d85", "very cool content", new DateTime(2024, 9, 20, 19, 57, 34, 16, DateTimeKind.Local).AddTicks(8194), "Very cool article" },
                    { 2, "1927b008-f316-4535-9328-958997f88a39", "Lots of article 2 content here", new DateTime(2024, 9, 20, 19, 57, 34, 16, DateTimeKind.Local).AddTicks(8261), "Article 2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "40d75bab-de0d-480f-9077-f7c672b2b9df", "1927b008-f316-4535-9328-958997f88a39" },
                    { "40d75bab-de0d-480f-9077-f7c672b2b9df", "57a0fb60-4421-46d1-9931-9a54b3347d85" },
                    { "d1ec52b1-6223-42f6-85b7-8af22edb439c", "69e928e3-4b49-42e2-a6bb-3724400398ab" },
                    { "d1ec52b1-6223-42f6-85b7-8af22edb439c", "e55487c1-37f7-4cad-88f1-13c089ceb2c6" },
                    { "a053fdb1-2e00-4cc9-996e-d5a7f5a244fa", "faa054ae-0eb7-44db-b34f-10bc5fc9fe09" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "AuthorId", "Content" },
                values: new object[,]
                {
                    { 1, 1, "e55487c1-37f7-4cad-88f1-13c089ceb2c6", "I like this :)" },
                    { 2, 1, "69e928e3-4b49-42e2-a6bb-3724400398ab", "This is fake news" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
