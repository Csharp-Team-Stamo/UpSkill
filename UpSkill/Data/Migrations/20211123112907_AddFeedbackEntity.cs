using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddFeedbackEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSlots_Coaches_CoachId",
                table: "SessionSlots");

            migrationBuilder.DropTable(
                name: "CoachVotes");

            migrationBuilder.DropIndex(
                name: "IX_SessionSlots_CoachId",
                table: "SessionSlots");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "SessionSlots");

            migrationBuilder.AddColumn<int>(
                name: "CoachFeedbackId",
                table: "LiveSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GivenFeedback",
                table: "LiveSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CoachFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachFeedback", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions",
                column: "CoachFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachFeedback_IsDeleted",
                table: "CoachFeedback",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveSessions_CoachFeedback_CoachFeedbackId",
                table: "LiveSessions",
                column: "CoachFeedbackId",
                principalTable: "CoachFeedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveSessions_CoachFeedback_CoachFeedbackId",
                table: "LiveSessions");

            migrationBuilder.DropTable(
                name: "CoachFeedback");

            migrationBuilder.DropIndex(
                name: "IX_LiveSessions_CoachFeedbackId",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "CoachFeedbackId",
                table: "LiveSessions");

            migrationBuilder.DropColumn(
                name: "GivenFeedback",
                table: "LiveSessions");

            migrationBuilder.AddColumn<string>(
                name: "CoachId",
                table: "SessionSlots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoachVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachVotes_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSlots_CoachId",
                table: "SessionSlots",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachVotes_CoachId",
                table: "CoachVotes",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachVotes_IsDeleted",
                table: "CoachVotes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CoachVotes_UserId",
                table: "CoachVotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSlots_Coaches_CoachId",
                table: "SessionSlots",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
