using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class ChangTenVPToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenVP",
                table: "VanPhongs",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "nvarchar(50)",
                table: "BanGiaos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "VanPhongs",
                newName: "TenVP");

            migrationBuilder.AlterColumn<string>(
                name: "nvarchar(50)",
                table: "BanGiaos",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
