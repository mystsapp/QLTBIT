using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addKhuVucCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KhuVuc",
                table: "ChiNhanhs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhuVuc",
                table: "ChiNhanhs");
        }
    }
}
