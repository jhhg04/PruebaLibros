using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaLibros.Infraestructura.Data.MigracionesEF
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    IdAutorPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.IdAutorPk);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    IdLibroPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    NumPaginas = table.Column<int>(type: "int", nullable: false),
                    IdAutorFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.IdLibroPk);
                    table.ForeignKey(
                        name: "FK_Libro_Autor_IdAutorFk",
                        column: x => x.IdAutorFk,
                        principalTable: "Autor",
                        principalColumn: "IdAutorPk",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libro_IdAutorFk",
                table: "Libro",
                column: "IdAutorFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Autor");
        }
    }
}
