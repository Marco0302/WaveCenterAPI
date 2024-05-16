using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class TabelaExperiencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaExperiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaExperiencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoExperiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExperiencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroMinimoPessoas = table.Column<int>(type: "int", nullable: false),
                    NumeroMaximoPessoas = table.Column<int>(type: "int", nullable: false),
                    IdTipoExperiencia = table.Column<int>(type: "int", nullable: false),
                    IdCategoriaExperiencia = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencias_CategoriaExperiencias_IdCategoriaExperiencia",
                        column: x => x.IdCategoriaExperiencia,
                        principalTable: "CategoriaExperiencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiencias_TipoExperiencias_IdTipoExperiencia",
                        column: x => x.IdTipoExperiencia,
                        principalTable: "TipoExperiencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdCategoriaExperiencia",
                table: "Experiencias",
                column: "IdCategoriaExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdTipoExperiencia",
                table: "Experiencias",
                column: "IdTipoExperiencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiencias");

            migrationBuilder.DropTable(
                name: "CategoriaExperiencias");

            migrationBuilder.DropTable(
                name: "TipoExperiencias");
        }
    }
}
