using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask.Migrations.Migrations
{
    public partial class AuthorImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Authors");
        }
    }
}
