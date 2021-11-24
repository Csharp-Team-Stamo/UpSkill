using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class RemoveCategoryPropertyFromLiveSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveSessions_Categories_CategoryId",
                table: "LiveSessions");

            migrationBuilder.DropIndex(
                name: "IX_LiveSessions_CategoryId",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "LiveSessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "LiveSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_CategoryId",
                table: "LiveSessions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveSessions_Categories_CategoryId",
                table: "LiveSessions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
