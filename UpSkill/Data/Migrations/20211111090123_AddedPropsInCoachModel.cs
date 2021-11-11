using Microsoft.EntityFrameworkCore.Migrations;

namespace UpSkill.Data.Migrations
{
    public partial class AddedPropsInCoachModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarImgUrl",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscussionDurationInMinutes",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourcesCount",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionDescription",
                table: "Coaches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillsLearn",
                table: "Coaches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImgUrl",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "DiscussionDurationInMinutes",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "ResourcesCount",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "SessionDescription",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "SkillsLearn",
                table: "Coaches");
        }
    }
}
