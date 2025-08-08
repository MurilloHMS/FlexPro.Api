using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResyncWithModel : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "produto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Veiculo",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Revisao",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "produto",
                newName: "id");

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
                table: "Entidade",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Embalagem",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contato",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categoria",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Categoria",
                newName: "name");

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

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Categoria",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Uf",
                table: "Abastecimento",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Abastecimento",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NomeDoMotorista",
                table: "Abastecimento",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Combustivel",
                table: "Abastecimento",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produto",
                table: "produto",
                column: "id");

            migrationBuilder.CreateTable(
                name: "archives",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    file_extensions = table.Column<string>(type: "varchar", maxLength: 6, nullable: true),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archives", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emails_smtp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails_smtp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    system_code = table.Column<string>(type: "varchar", maxLength: 10, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_movements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    system_code = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    minimum_stock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_products", x => x.id);
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

            migrationBuilder.DropTable(
                name: "archives");

            migrationBuilder.DropTable(
                name: "emails_smtp");

            migrationBuilder.DropTable(
                name: "inventory_movements");

            migrationBuilder.DropTable(
                name: "inventory_products");

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
                table: "Veiculo",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Revisao",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Produto",
                newName: "Id");

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
                table: "Entidade",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Embalagem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Contato",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categoria",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categoria",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AcessoRemoto",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Uf",
                table: "Abastecimento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Abastecimento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeDoMotorista",
                table: "Abastecimento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Combustivel",
                table: "Abastecimento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

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
        }
    }
}
