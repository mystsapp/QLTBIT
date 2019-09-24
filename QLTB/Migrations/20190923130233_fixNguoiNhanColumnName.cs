using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class fixNguoiNhanColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nvarchar(50)",
                table: "BanGiaos",
                newName: "NguoiNhan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NguoiNhan",
                table: "BanGiaos",
                newName: "nvarchar(50)");
        }
    }
}
