using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddedUserToVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseVote",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CoachVote",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseVote_UserId",
                table: "CourseVote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachVote_UserId",
                table: "CoachVote",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachVote_AspNetUsers_UserId",
                table: "CoachVote",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVote_AspNetUsers_UserId",
                table: "CourseVote",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachVote_AspNetUsers_UserId",
                table: "CoachVote");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVote_AspNetUsers_UserId",
                table: "CourseVote");

            migrationBuilder.DropIndex(
                name: "IX_CourseVote_UserId",
                table: "CourseVote");

            migrationBuilder.DropIndex(
                name: "IX_CoachVote_UserId",
                table: "CoachVote");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseVote");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CoachVote");
        }
    }
}
