using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class CourseOwnerRemovedBaseDeleteInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOwners",
                table: "CourseOwners");

            migrationBuilder.DropIndex(
                name: "IX_CourseOwners_CourseId",
                table: "CourseOwners");

            migrationBuilder.DropIndex(
                name: "IX_CourseOwners_IsDeleted",
                table: "CourseOwners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseOwners");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CourseOwners");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CourseOwners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseOwners");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CourseOwners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "CourseOwners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOwners",
                table: "CourseOwners",
                columns: new[] { "CourseId", "OwnerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOwners",
                table: "CourseOwners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "CourseOwners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseOwners",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CourseOwners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CourseOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseOwners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CourseOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOwners",
                table: "CourseOwners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOwners_CourseId",
                table: "CourseOwners",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOwners_IsDeleted",
                table: "CourseOwners",
                column: "IsDeleted");
        }
    }
}
