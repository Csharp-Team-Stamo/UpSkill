using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class RemovedBaseDeleteInheritanceFromCoachLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachLanguages",
                table: "CoachLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CoachLanguages_CoachId",
                table: "CoachLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CoachLanguages_IsDeleted",
                table: "CoachLanguages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoachLanguages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CoachLanguages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CoachLanguages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CoachLanguages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CoachLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachLanguages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachLanguages",
                table: "CoachLanguages",
                columns: new[] { "CoachId", "LanguageId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachLanguages",
                table: "CoachLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachLanguages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoachLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CoachLanguages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CoachLanguages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CoachLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CoachLanguages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachLanguages",
                table: "CoachLanguages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLanguages_CoachId",
                table: "CoachLanguages",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLanguages_IsDeleted",
                table: "CoachLanguages",
                column: "IsDeleted");
        }
    }
}
