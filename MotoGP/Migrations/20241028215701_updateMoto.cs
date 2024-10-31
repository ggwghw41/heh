using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Migrations
{
    /// <inheritdoc />
    public partial class updateMoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<bool>(
                name: "NoiBat",
                table: "MoTo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "MoTo");

            migrationBuilder.DropColumn(
                name: "NoiBat",
                table: "MoTo");
        }
    }
}
