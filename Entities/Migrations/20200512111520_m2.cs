using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "MenuItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RoleId",
                table: "MenuItems",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId",
                table: "MenuItems",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_RoleId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
