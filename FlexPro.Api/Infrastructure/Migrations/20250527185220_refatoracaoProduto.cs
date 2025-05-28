using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class refatoracaoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Receita_IdReceita",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdReceita",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdReceita",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "QuantidadeFormula",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "MateriaPrima",
                table: "Produto",
                newName: "Nome");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Produto",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeProducao",
                table: "Produto",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produto",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Produto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diluicao",
                table: "Produto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Produto",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoEstoque",
                table: "Produto",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMateriaPrima",
                table: "Produto",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Embalagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    TipoEmbalagem = table.Column<int>(type: "integer", nullable: false),
                    Tamanho = table.Column<int>(type: "integer", nullable: false),
                    ProdutoLojaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embalagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Embalagem_Produto_ProdutoLojaId",
                        column: x => x.ProdutoLojaId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaMateriaPrima",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(type: "integer", nullable: false),
                    MateriaPrimaId = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeFormula = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaMateriaPrima", x => new { x.ReceitaId, x.MateriaPrimaId });
                    table.ForeignKey(
                        name: "FK_ReceitaMateriaPrima_Produto_MateriaPrimaId",
                        column: x => x.MateriaPrimaId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaMateriaPrima_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartamentoProdutoLoja",
                columns: table => new
                {
                    DepartamentosId = table.Column<int>(type: "integer", nullable: false),
                    ProdutosLojaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentoProdutoLoja", x => new { x.DepartamentosId, x.ProdutosLojaId });
                    table.ForeignKey(
                        name: "FK_DepartamentoProdutoLoja_Departamento_DepartamentosId",
                        column: x => x.DepartamentosId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartamentoProdutoLoja_Produto_ProdutosLojaId",
                        column: x => x.ProdutosLojaId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentoProdutoLoja_ProdutosLojaId",
                table: "DepartamentoProdutoLoja",
                column: "ProdutosLojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Embalagem_ProdutoLojaId",
                table: "Embalagem",
                column: "ProdutoLojaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaMateriaPrima_MateriaPrimaId",
                table: "ReceitaMateriaPrima",
                column: "MateriaPrimaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartamentoProdutoLoja");

            migrationBuilder.DropTable(
                name: "Embalagem");

            migrationBuilder.DropTable(
                name: "ReceitaMateriaPrima");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Diluicao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TipoEstoque",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TipoMateriaPrima",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produto",
                newName: "MateriaPrima");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Produto",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeProducao",
                table: "Produto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produto",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdReceita",
                table: "Produto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantidadeFormula",
                table: "Produto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdReceita",
                table: "Produto",
                column: "IdReceita");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Receita_IdReceita",
                table: "Produto",
                column: "IdReceita",
                principalTable: "Receita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
