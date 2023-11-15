using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyVet.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacunaRefId",
                table: "Detalle",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_VacunaRefId",
                table: "Detalle",
                column: "VacunaRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_Vacunas_VacunaRefId",
                table: "Detalle",
                column: "VacunaRefId",
                principalTable: "Vacunas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_Vacunas_VacunaRefId",
                table: "Detalle");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_VacunaRefId",
                table: "Detalle");

            migrationBuilder.DropColumn(
                name: "VacunaRefId",
                table: "Detalle");
        }
    }
}
