using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    public partial class ChangeUserPhotosDbName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "UserPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_UserId",
                table: "UserPhotos",
                newName: "IX_UserPhotos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_AspNetUsers_UserId",
                table: "UserPhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_AspNetUsers_UserId",
                table: "UserPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos");

            migrationBuilder.RenameTable(
                name: "UserPhotos",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhotos_UserId",
                table: "Photos",
                newName: "IX_Photos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
