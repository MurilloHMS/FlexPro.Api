using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Veiculos_VeiculoId",
                table: "Revisao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory-products",
                table: "inventory-products");

            migrationBuilder.RenameTable(
                name: "Veiculos",
                newName: "Veiculo");

            migrationBuilder.RenameTable(
                name: "inventory-products",
                newName: "inventory_products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "inventory_movements",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "outro",
                table: "Contato",
                newName: "Outro");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categoria",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Categoria",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "system_code",
                table: "inventory_movements",
                type: "varchar",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "inventory_movements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AlterColumn<int>(
                name: "minimum_stock",
                table: "inventory_products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_products",
                table: "inventory_products",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Veiculo_VeiculoId",
                table: "Revisao",
                column: "VeiculoId",
                principalTable: "Veiculo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisao_Veiculo_VeiculoId",
                table: "Revisao");

            migrationBuilder.DropTable(
                name: "archives");

            migrationBuilder.DropTable(
                name: "emails_smtp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_products",
                table: "inventory_products");

            migrationBuilder.RenameTable(
                name: "Veiculo",
                newName: "Veiculos");

            migrationBuilder.RenameTable(
                name: "inventory_products",
                newName: "inventory-products");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "inventory_movements",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Outro",
                table: "Contato",
                newName: "outro");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categoria",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categoria",
                newName: "Nome");

            migrationBuilder.AlterColumn<string>(
                name: "system_code",
                table: "inventory_movements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "inventory_movements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "minimum_stock",
                table: "inventory-products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory-products",
                table: "inventory-products",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisao_Veiculos_VeiculoId",
                table: "Revisao",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
