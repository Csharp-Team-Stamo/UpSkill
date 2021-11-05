using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class RemovedBaseDeleteInhertanceFromCoachEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachEmployees",
                table: "CoachEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CoachEmployees_CoachId",
                table: "CoachEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CoachEmployees_IsDeleted",
                table: "CoachEmployees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoachEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CoachEmployees");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CoachEmployees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CoachEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CoachEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CoachEmployees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachEmployees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachEmployees",
                table: "CoachEmployees",
                columns: new[] { "CoachId", "StudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachEmployees",
                table: "CoachEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CoachEmployees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachEmployees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoachEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CoachEmployees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CoachEmployees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CoachEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CoachEmployees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachEmployees",
                table: "CoachEmployees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CoachEmployees_CoachId",
                table: "CoachEmployees",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachEmployees_IsDeleted",
                table: "CoachEmployees",
                column: "IsDeleted");
        }
    }
}
