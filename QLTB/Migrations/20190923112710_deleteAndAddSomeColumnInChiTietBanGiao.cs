using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class deleteAndAddSomeColumnInChiTietBanGiao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiNhap",
                table: "ChiTietBanGiaos");

            migrationBuilder.RenameColumn(
                name: "NgayNhap",
                table: "ChiTietBanGiaos",
                newName: "BaoHanhDen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaoHanhDen",
                table: "ChiTietBanGiaos",
                newName: "NgayNhap");

            migrationBuilder.AddColumn<string>(
                name: "NguoiNhap",
                table: "ChiTietBanGiaos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
