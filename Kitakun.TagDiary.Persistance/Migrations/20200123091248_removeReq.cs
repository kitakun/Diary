using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitakun.TagDiary.Persistance.Migrations
{
    public partial class removeReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MasterPasswordHash",
                table: "SpaceOwners",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MasterPasswordHash",
                table: "SpaceOwners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
