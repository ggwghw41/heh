using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Migrations
{
    /// <inheritdoc />
    public partial class AddMotoImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotoImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMoTo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotoImages_MoTo_MotoId",
                        column: x => x.MotoId,
                        principalTable: "MoTo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotoImages_MotoId",
                table: "MotoImages",
                column: "MotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotoImages");
        }
    }
}
