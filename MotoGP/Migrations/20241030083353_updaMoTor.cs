using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Migrations
{
    /// <inheritdoc />
    public partial class updaMoTor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HieuSuat",
                table: "MoTo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HieuSuat",
                table: "MoTo");
        }
    }
}
