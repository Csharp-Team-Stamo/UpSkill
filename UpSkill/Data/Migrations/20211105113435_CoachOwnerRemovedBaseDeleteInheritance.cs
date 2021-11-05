using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class CoachOwnerRemovedBaseDeleteInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachOwners",
                table: "CoachOwners");

            migrationBuilder.DropIndex(
                name: "IX_CoachOwners_CoachId",
                table: "CoachOwners");

            migrationBuilder.DropIndex(
                name: "IX_CoachOwners_IsDeleted",
                table: "CoachOwners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoachOwners");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CoachOwners");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CoachOwners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CoachOwners");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CoachOwners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "CoachOwners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachOwners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachOwners",
                table: "CoachOwners",
                columns: new[] { "CoachId", "OwnerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachOwners",
                table: "CoachOwners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "CoachOwners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "CoachOwners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoachOwners",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CoachOwners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CoachOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CoachOwners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CoachOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachOwners",
                table: "CoachOwners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CoachOwners_CoachId",
                table: "CoachOwners",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachOwners_IsDeleted",
                table: "CoachOwners",
                column: "IsDeleted");
        }
    }
}
