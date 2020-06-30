using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class roleid_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NationalityMaster_FK_Employee_Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId1",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_RoleId1",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FK_Employee_Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "FK_Employee_Nationality",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "MenuItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9e07e01f-d11e-425c-b2bb-b94a86885a6d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2bcc27b0-07df-4b68-908d-4fbb6b4c0da3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "63cf928b-0548-4231-8315-761ec0ac4d5c");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RoleId",
                table: "MenuItems",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NationalityMaster_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId",
                principalTable: "NationalityMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId",
                table: "MenuItems",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NationalityMaster_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_RoleId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_Employee_Nationality",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "685a840a-e628-4489-b2f4-e3498a65cbdf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "494bfe00-5794-4f0d-824c-c0704c24231b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8cda3315-b898-4137-b335-05a399b1bdab");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RoleId1",
                table: "MenuItems",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FK_Employee_Nationality",
                table: "AspNetUsers",
                column: "FK_Employee_Nationality");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NationalityMaster_FK_Employee_Nationality",
                table: "AspNetUsers",
                column: "FK_Employee_Nationality",
                principalTable: "NationalityMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_AspNetRoles_RoleId1",
                table: "MenuItems",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
