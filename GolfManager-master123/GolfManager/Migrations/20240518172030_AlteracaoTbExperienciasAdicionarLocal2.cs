using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoTbExperienciasAdicionarLocal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DuracaoMaxima",
                table: "Experiencias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DuracaoMinima",
                table: "Experiencias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HoraComecoDia",
                table: "Experiencias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HoraFimDia",
                table: "Experiencias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "Experiencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PrecoHora",
                table: "Experiencias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiencias_IdLocal",
                table: "Experiencias",
                column: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiencias_Locais_IdLocal",
                table: "Experiencias",
                column: "IdLocal",
                principalTable: "Locais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiencias_Locais_IdLocal",
                table: "Experiencias");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Experiencias_IdLocal",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "DuracaoMaxima",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "DuracaoMinima",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "HoraComecoDia",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "HoraFimDia",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "Experiencias");

            migrationBuilder.DropColumn(
                name: "PrecoHora",
                table: "Experiencias");
        }
    }
}
