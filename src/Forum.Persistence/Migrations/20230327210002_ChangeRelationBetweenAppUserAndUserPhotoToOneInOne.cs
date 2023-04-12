using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    public partial class ChangeRelationBetweenAppUserAndUserPhotoToOneInOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "UserPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "UserPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos",
                column: "UserId");
        }
    }
}
