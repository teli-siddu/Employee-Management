using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class rolemenu_table_name_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenu_MenuItems_MenuItemId",
                table: "RoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenu_AspNetRoles_RoleId",
                table: "RoleMenu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenu",
                table: "RoleMenu");

            migrationBuilder.RenameTable(
                name: "RoleMenu",
                newName: "RoleMenus");

            migrationBuilder.RenameIndex(
                name: "IX_RoleMenu_RoleId",
                table: "RoleMenus",
                newName: "IX_RoleMenus_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleMenu_MenuItemId",
                table: "RoleMenus",
                newName: "IX_RoleMenus_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenus",
                table: "RoleMenus",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "70cc2155-8512-4300-8664-7c0582cb2c33");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8e6c8d67-68e1-470b-8341-20fe5f4fc6c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7da5ce08-e51b-4b2b-b749-7d8492e9c793");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenus_MenuItems_MenuItemId",
                table: "RoleMenus",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenus_AspNetRoles_RoleId",
                table: "RoleMenus",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenus_MenuItems_MenuItemId",
                table: "RoleMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenus_AspNetRoles_RoleId",
                table: "RoleMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenus",
                table: "RoleMenus");

            migrationBuilder.RenameTable(
                name: "RoleMenus",
                newName: "RoleMenu");

            migrationBuilder.RenameIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenu",
                newName: "IX_RoleMenu_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleMenus_MenuItemId",
                table: "RoleMenu",
                newName: "IX_RoleMenu_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenu",
                table: "RoleMenu",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f4874a55-c83a-4630-91ba-028aa710d6c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bce0c3e2-e635-46e8-bd02-bec4f5f74052");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3390f517-fe78-4d33-9342-8c74272a50a5");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenu_MenuItems_MenuItemId",
                table: "RoleMenu",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenu_AspNetRoles_RoleId",
                table: "RoleMenu",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
