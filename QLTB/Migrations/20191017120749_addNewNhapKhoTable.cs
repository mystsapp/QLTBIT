using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addNewNhapKhoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhapKhos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenThietBi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DienGiaiTB = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NguoiNhapKho = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NgaySuDung = table.Column<DateTime>(nullable: true),
                    NguoiSuDung = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NgayNhapKho = table.Column<DateTime>(nullable: true),
                    KhoVanPhong = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ThanhLy = table.Column<bool>(nullable: false),
                    NgayThanhLy = table.Column<DateTime>(nullable: true),
                    NguoiThanhLy = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhapKhos");
        }
    }
}
