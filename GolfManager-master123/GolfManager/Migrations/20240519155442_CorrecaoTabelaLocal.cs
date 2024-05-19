using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTabelaLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locais",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Coordenadas",
                table: "Locais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordenadas",
                table: "Locais");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Locais",
                newName: "Name");
        }
    }
}
