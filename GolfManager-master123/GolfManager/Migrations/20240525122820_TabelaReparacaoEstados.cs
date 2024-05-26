using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class TabelaReparacaoEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPedidoReparacao",
                table: "Equipamentos");

            migrationBuilder.AddColumn<int>(
                name: "ExperienciaId",
                table: "Marcacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PedidoReparacaoEstados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPedidoReparacao = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoReparacaoEstados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoReparacaoEstados_PedidoReparacao_IdPedidoReparacao",
                        column: x => x.IdPedidoReparacao,
                        principalTable: "PedidoReparacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_ExperienciaId",
                table: "Marcacoes",
                column: "ExperienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoReparacaoEstados_IdPedidoReparacao",
                table: "PedidoReparacaoEstados",
                column: "IdPedidoReparacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Marcacoes_Experiencias_ExperienciaId",
                table: "Marcacoes",
                column: "ExperienciaId",
                principalTable: "Experiencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marcacoes_Experiencias_ExperienciaId",
                table: "Marcacoes");

            migrationBuilder.DropTable(
                name: "PedidoReparacaoEstados");

            migrationBuilder.DropIndex(
                name: "IX_Marcacoes_ExperienciaId",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "ExperienciaId",
                table: "Marcacoes");

            migrationBuilder.AddColumn<int>(
                name: "IdPedidoReparacao",
                table: "Equipamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
