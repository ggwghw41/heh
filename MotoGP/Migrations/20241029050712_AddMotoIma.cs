using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Migrations
{
    /// <inheritdoc />
    public partial class AddMotoIma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotoImages_MoTo_MotoId",
                table: "MotoImages");

            migrationBuilder.DropIndex(
                name: "IX_MotoImages_MotoId",
                table: "MotoImages");

            migrationBuilder.DropColumn(
                name: "MotoId",
                table: "MotoImages");

            migrationBuilder.CreateIndex(
                name: "IX_MotoImages_IdMoTo",
                table: "MotoImages",
                column: "IdMoTo");

            migrationBuilder.AddForeignKey(
                name: "FK_MotoImages_MoTo_IdMoTo",
                table: "MotoImages",
                column: "IdMoTo",
                principalTable: "MoTo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotoImages_MoTo_IdMoTo",
                table: "MotoImages");

            migrationBuilder.DropIndex(
                name: "IX_MotoImages_IdMoTo",
                table: "MotoImages");

            migrationBuilder.AddColumn<Guid>(
                name: "MotoId",
                table: "MotoImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MotoImages_MotoId",
                table: "MotoImages",
                column: "MotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MotoImages_MoTo_MotoId",
                table: "MotoImages",
                column: "MotoId",
                principalTable: "MoTo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
