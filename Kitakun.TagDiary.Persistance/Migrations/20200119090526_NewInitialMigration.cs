using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Kitakun.TagDiary.Persistance.Migrations
{
    public partial class NewInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UrlName = table.Column<string>(nullable: true),
                    BlogPrivacy = table.Column<byte>(nullable: false),
                    MasterPasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TokenUrl = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    MarkdownText = table.Column<string>(nullable: false),
                    Privacy = table.Column<byte>(nullable: false),
                    ProtectedByPassword = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<int>(nullable: true),
                    Tags = table.Column<string[]>(type: "varchar(255)[]", nullable: true),
                    SpaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryRecords_SpaceOwners_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "SpaceOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpaceOwnerTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AllTagsInSpace = table.Column<string[]>(type: "varchar(255)[]", nullable: true),
                    SpaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceOwnerTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceOwnerTags_SpaceOwners_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "SpaceOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryRecords_SpaceId",
                table: "DiaryRecords",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceOwnerTags_SpaceId",
                table: "SpaceOwnerTags",
                column: "SpaceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryRecords");

            migrationBuilder.DropTable(
                name: "SpaceOwnerTags");

            migrationBuilder.DropTable(
                name: "SpaceOwners");
        }
    }
}
