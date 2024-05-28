using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class EquipamentosMarcacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipamentosMarcacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMarcacao = table.Column<int>(type: "int", nullable: false),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false),
                    IdEquipamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamentosMarcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipamentosMarcacoes_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipamentosMarcacoes_Marcacoes_IdEquipamento",
                        column: x => x.IdEquipamento,
                        principalTable: "Marcacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosMarcacoes_EquipamentoId",
                table: "EquipamentosMarcacoes",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentosMarcacoes_IdEquipamento",
                table: "EquipamentosMarcacoes",
                column: "IdEquipamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipamentosMarcacoes");
        }
    }
}
