using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddLiveSessionProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelationUri",
                table: "LiveSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventSessionType",
                table: "LiveSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JoinSessionUri",
                table: "LiveSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReschedulingUri",
                table: "LiveSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelationUri",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "EventSessionType",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "JoinSessionUri",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "ReschedulingUri",
                table: "LiveSessions");
        }
    }
}
