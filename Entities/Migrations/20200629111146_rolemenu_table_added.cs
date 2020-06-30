using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class rolemenu_table_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_RoleId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuType",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "ProfilePictures",
                newName: "Path");

            migrationBuilder.CreateTable(
                name: "RoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenu_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenu_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_MenuItemId",
                table: "RoleMenu",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_RoleId",
                table: "RoleMenu",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMenu");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "ProfilePictures",
                newName: "path");

            migrationBuilder.AddColumn<int>(
                name: "MenuType",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8220accc-b542-484a-8c24-92d80c2103f7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fe42a726-8ddb-4169-b79c-e28eb07105c7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b115a557-333d-4ca1-b48e-08d48397a5b9");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
