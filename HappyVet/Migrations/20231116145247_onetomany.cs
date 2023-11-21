using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyVet.Migrations
{
    /// <inheritdoc />
    public partial class onetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraConsulta = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RegistroMascotaRefId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consulta_Registro de Mascota_RegistroMascotaRefId",
                        column: x => x.RegistroMascotaRefId,
                        principalTable: "Registro de Mascota",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaVacunas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacunaRefId = table.Column<int>(type: "int", nullable: false),
                    ConsultaId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaVacunas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultaVacunas_Consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consulta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsultaVacunas_Vacunas_VacunaRefId",
                        column: x => x.VacunaRefId,
                        principalTable: "Vacunas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_RegistroMascotaRefId",
                table: "Consulta",
                column: "RegistroMascotaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaVacunas_ConsultaId",
                table: "ConsultaVacunas",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaVacunas_VacunaRefId",
                table: "ConsultaVacunas",
                column: "VacunaRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultaVacunas");

            migrationBuilder.DropTable(
                name: "Consulta");
        }
    }
}
