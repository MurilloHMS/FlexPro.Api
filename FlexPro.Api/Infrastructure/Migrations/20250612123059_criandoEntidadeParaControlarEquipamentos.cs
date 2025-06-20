using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class criandoEntidadeParaControlarEquipamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Interno = table.Column<bool>(type: "boolean", nullable: true),
                    Marca = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcessoRemoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    TipoAcessoRemoto = table.Column<int>(type: "integer", nullable: false),
                    Conexao = table.Column<string>(type: "text", nullable: false),
                    IdComputador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessoRemoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcessoRemoto_Equipamento_IdComputador",
                        column: x => x.IdComputador,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Processador = table.Column<string>(type: "text", nullable: false),
                    MemoriaRAM = table.Column<int>(type: "integer", nullable: false),
                    Armazenamento = table.Column<int>(type: "integer", nullable: false),
                    TipoArmazenamento = table.Column<string>(type: "text", nullable: false),
                    IdComputador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especificacoes_Equipamento_IdComputador",
                        column: x => x.IdComputador,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessoRemoto_IdComputador",
                table: "AcessoRemoto",
                column: "IdComputador");

            migrationBuilder.CreateIndex(
                name: "IX_Especificacoes_IdComputador",
                table: "Especificacoes",
                column: "IdComputador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcessoRemoto");

            migrationBuilder.DropTable(
                name: "Especificacoes");

            migrationBuilder.DropTable(
                name: "Equipamento");
        }
    }
}
