using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addGhiChuToLoaiPM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "LoaiPhanMems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "LoaiPhanMems");
        }
    }
}
