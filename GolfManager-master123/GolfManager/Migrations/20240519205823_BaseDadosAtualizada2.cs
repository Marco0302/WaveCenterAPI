using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveCenter.Migrations
{
    /// <inheritdoc />
    public partial class BaseDadosAtualizada2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TipoUsers",
                newName: "Designacao");

            migrationBuilder.AlterColumn<string>(
                name: "Designacao",
                table: "TipoExperiencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Designacao",
                table: "TipoUsers",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Designacao",
                table: "TipoExperiencias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
