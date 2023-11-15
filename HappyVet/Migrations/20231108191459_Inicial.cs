using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyVet.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Raza",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raza", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tamaño",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamaño", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo Animal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo Animal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Registro de Mascota",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagemMascota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoAnimalRefId = table.Column<int>(type: "int", nullable: true),
                    RazaRefId = table.Column<int>(type: "int", nullable: true),
                    TamañoRefId = table.Column<int>(type: "int", nullable: true),
                    EdadRefId = table.Column<int>(type: "int", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro de Mascota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Registro de Mascota_Edad_EdadRefId",
                        column: x => x.EdadRefId,
                        principalTable: "Edad",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Registro de Mascota_Raza_RazaRefId",
                        column: x => x.RazaRefId,
                        principalTable: "Raza",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Registro de Mascota_Tamaño_TamañoRefId",
                        column: x => x.TamañoRefId,
                        principalTable: "Tamaño",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Registro de Mascota_Tipo Animal_TipoAnimalRefId",
                        column: x => x.TipoAnimalRefId,
                        principalTable: "Tipo Animal",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registro de Mascota_EdadRefId",
                table: "Registro de Mascota",
                column: "EdadRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Registro de Mascota_RazaRefId",
                table: "Registro de Mascota",
                column: "RazaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Registro de Mascota_TamañoRefId",
                table: "Registro de Mascota",
                column: "TamañoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Registro de Mascota_TipoAnimalRefId",
                table: "Registro de Mascota",
                column: "TipoAnimalRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registro de Mascota");

            migrationBuilder.DropTable(
                name: "Edad");

            migrationBuilder.DropTable(
                name: "Raza");

            migrationBuilder.DropTable(
                name: "Tamaño");

            migrationBuilder.DropTable(
                name: "Tipo Animal");
        }
    }
}
