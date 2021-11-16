using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddedNewPropInAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageToBase64",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "ImageToBase64",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageToBase64",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImageToBase64",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
