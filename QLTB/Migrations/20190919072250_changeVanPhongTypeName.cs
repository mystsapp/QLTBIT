using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class changeVanPhongTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nvarchar(50)",
                table: "NhanViens",
                newName: "VanPhong");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VanPhong",
                table: "NhanViens",
                newName: "nvarchar(50)");
        }
    }
}
