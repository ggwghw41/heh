using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Migrations
{
    /// <inheritdoc />
    public partial class updaMoTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Gia",
                table: "MoTo",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ChiTietDongCo",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChiTietHẹThongPhanh",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CongNghe",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DanhGia",
                table: "MoTo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SanXuat",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThietKe",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiTietDongCo",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "ChiTietHẹThongPhanh",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "CongNghe",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "DanhGia",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "SanXuat",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "ThietKe",
                table: "MoTo");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gia",
                table: "MoTo",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
