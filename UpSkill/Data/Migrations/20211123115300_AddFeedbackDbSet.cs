using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddFeedbackDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions");

            migrationBuilder.AddColumn<string>(
                name: "LiveSessionId",
                table: "CoachFeedback",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions",
                column: "CoachFeedbackId",
                unique: true,
                filter: "[CoachFeedbackId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "LiveSessionId",
                table: "CoachFeedback");

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions",
                column: "CoachFeedbackId");
        }
    }
}
