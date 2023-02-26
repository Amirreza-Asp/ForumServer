using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    public partial class AddPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_AspNetUsers_AppUserId",
                table: "UserPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhoto",
                table: "UserPhoto");

            migrationBuilder.DropIndex(
                name: "IX_UserPhoto_AppUserId",
                table: "UserPhoto");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserPhoto");

            migrationBuilder.RenameTable(
                name: "UserPhoto",
                newName: "Photos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "UserPhoto");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "UserPhoto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhoto",
                table: "UserPhoto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_AppUserId",
                table: "UserPhoto",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_AspNetUsers_AppUserId",
                table: "UserPhoto",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
