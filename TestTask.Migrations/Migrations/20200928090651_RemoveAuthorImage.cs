using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask.Migrations.Migrations
{
    public partial class RemoveAuthorImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Authors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
