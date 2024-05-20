using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class NovasAtualizacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TipoUsers_IdTipoUser",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Experiencias",
                newName: "DataInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Marcacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Experiencias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoUser",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TipoUsers_IdTipoUser",
                table: "AspNetUsers",
                column: "IdTipoUser",
                principalTable: "TipoUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TipoUsers_IdTipoUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Experiencias");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Experiencias",
                newName: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoUser",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TipoUsers_IdTipoUser",
                table: "AspNetUsers",
                column: "IdTipoUser",
                principalTable: "TipoUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
