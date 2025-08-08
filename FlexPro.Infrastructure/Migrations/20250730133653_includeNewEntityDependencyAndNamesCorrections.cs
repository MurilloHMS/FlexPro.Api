using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class includeNewEntityDependencyAndNamesCorrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartamentoProdutoLoja_Produto_ProdutosLojaId",
                table: "DepartamentoProdutoLoja");

            migrationBuilder.DropForeignKey(
                name: "FK_Embalagem_Produto_ProdutoLojaId",
                table: "Embalagem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaMateriaPrima_Produto_MateriaPrimaId",
                table: "ReceitaMateriaPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Entidade_LocalId",
                table: "Revisao");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Veiculo_VeiculoId",
                table: "Revisao");

            migrationBuilder.DropTable(
                name: "Entidade");

            migrationBuilder.DropTable(
                name: "inventory_products");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "produto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Revisao",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "produto",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "inventory_movements",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MemoriaRAM",
                table: "Especificacoes",
                newName: "MemoriaRam");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Equipamento",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Embalagem",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AcessoRemoto",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ReceitaMateriaPrima",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_produto",
                table: "produto",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Entidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CodigoSistema = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Contato = table.Column<string>(type: "text", nullable: true),
                    MeioDeContato = table.Column<int>(type: "integer", nullable: true),
                    RazaoSocial = table.Column<string>(type: "text", nullable: true),
                    EmailTeste = table.Column<string>(type: "text", nullable: true),
                    RecebeEmail = table.Column<bool>(type: "boolean", nullable: true),
                    Hierarquia = table.Column<int>(type: "integer", nullable: true),
                    Gerente = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    TipoVendedor = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidade", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inventory-products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    system_code = table.Column<string>(type: "text", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    minimum_stock = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory-products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    ConsumoUrbanoAlcool = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoUrbanoGasolina = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoRodoviarioAlcool = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoRodoviarioGasolina = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartamentoProdutoLoja_produto_ProdutosLojaId",
                table: "DepartamentoProdutoLoja",
                column: "ProdutosLojaId",
                principalTable: "produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Embalagem_produto_ProdutoLojaId",
                table: "Embalagem",
                column: "ProdutoLojaId",
                principalTable: "produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaMateriaPrima_produto_MateriaPrimaId",
                table: "ReceitaMateriaPrima",
                column: "MateriaPrimaId",
                principalTable: "produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Entidades_LocalId",
                table: "Revisao",
                column: "LocalId",
                principalTable: "Entidades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Veiculos_VeiculoId",
                table: "Revisao",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartamentoProdutoLoja_produto_ProdutosLojaId",
                table: "DepartamentoProdutoLoja");

            migrationBuilder.DropForeignKey(
                name: "FK_Embalagem_produto_ProdutoLojaId",
                table: "Embalagem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaMateriaPrima_produto_MateriaPrimaId",
                table: "ReceitaMateriaPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Entidades_LocalId",
                table: "Revisao");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Veiculos_VeiculoId",
                table: "Revisao");

            migrationBuilder.DropTable(
                name: "Entidade");

            migrationBuilder.DropTable(
                name: "inventory-products");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produto",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ReceitaMateriaPrima");

            migrationBuilder.RenameTable(
                name: "produto",
                newName: "Produto");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Revisao",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Produto",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "inventory_movements",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MemoriaRam",
                table: "Especificacoes",
                newName: "MemoriaRAM");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Equipamento",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Embalagem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AcessoRemoto",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Entidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CodigoSistema = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Contato = table.Column<string>(type: "text", nullable: true),
                    MeioDeContato = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    EmailTeste = table.Column<string>(type: "text", nullable: true),
                    RazaoSocial = table.Column<string>(type: "text", nullable: true),
                    RecebeEmail = table.Column<bool>(type: "boolean", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Gerente = table.Column<string>(type: "text", nullable: true),
                    Hierarquia = table.Column<int>(type: "integer", nullable: true),
                    TipoVendedor = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    minimum_stock = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    system_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumoRodoviarioAlcool = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoRodoviarioGasolina = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoUrbanoAlcool = table.Column<double>(type: "double precision", nullable: true),
                    ConsumoUrbanoGasolina = table.Column<double>(type: "double precision", nullable: true),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartamentoProdutoLoja_Produto_ProdutosLojaId",
                table: "DepartamentoProdutoLoja",
                column: "ProdutosLojaId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Embalagem_Produto_ProdutoLojaId",
                table: "Embalagem",
                column: "ProdutoLojaId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaMateriaPrima_Produto_MateriaPrimaId",
                table: "ReceitaMateriaPrima",
                column: "MateriaPrimaId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Entidade_LocalId",
                table: "Revisao",
                column: "LocalId",
                principalTable: "Entidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Veiculo_VeiculoId",
                table: "Revisao",
                column: "VeiculoId",
                principalTable: "Veiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
