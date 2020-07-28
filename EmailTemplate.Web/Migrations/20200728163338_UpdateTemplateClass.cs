using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailTemplate.Web.Migrations
{
    public partial class UpdateTemplateClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Template");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Template",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Template",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Template");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Template",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
