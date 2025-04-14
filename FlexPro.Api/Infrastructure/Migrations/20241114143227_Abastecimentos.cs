using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class Abastecimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abastecimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataDoAbastecimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Uf = table.Column<string>(type: "text", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    NomeDoMotorista = table.Column<string>(type: "text", nullable: false),
                    Departamento = table.Column<string>(type: "text", nullable: false),
                    HodometroAtual = table.Column<double>(type: "double precision", nullable: false),
                    HodometroAnterior = table.Column<double>(type: "double precision", nullable: false),
                    DiferencaHodometro = table.Column<double>(type: "double precision", nullable: false),
                    MediaKm = table.Column<double>(type: "double precision", nullable: false),
                    Combustivel = table.Column<string>(type: "text", nullable: false),
                    Litros = table.Column<double>(type: "double precision", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorTotalTransacao = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abastecimento", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abastecimento");
        }
    }
}
