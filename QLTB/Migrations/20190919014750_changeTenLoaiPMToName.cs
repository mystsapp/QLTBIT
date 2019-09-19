using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class changeTenLoaiPMToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenLoaiPM",
                table: "LoaiPhanMems",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LoaiPhanMems",
                newName: "TenLoaiPM");
        }
    }
}
