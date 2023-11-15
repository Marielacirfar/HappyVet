using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyVet.Migrations
{
    /// <inheritdoc />
    public partial class MigracionConContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListaPrecio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VacunaRefId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPrecio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListaPrecio_Vacunas_VacunaRefId",
                        column: x => x.VacunaRefId,
                        principalTable: "Vacunas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Detalle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioRefId = table.Column<int>(type: "int", nullable: true),
                    TarifaPrecio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Detalle_ListaPrecio_ListaPrecioRefId",
                        column: x => x.ListaPrecioRefId,
                        principalTable: "ListaPrecio",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_ListaPrecioRefId",
                table: "Detalle",
                column: "ListaPrecioRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPrecio_VacunaRefId",
                table: "ListaPrecio",
                column: "VacunaRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle");

            migrationBuilder.DropTable(
                name: "ListaPrecio");

            migrationBuilder.DropTable(
                name: "Vacunas");
        }
    }
}
