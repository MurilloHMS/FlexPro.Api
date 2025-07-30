using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Entidade",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CodigoSistema",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailTeste",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gerente",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hierarquia",
                table: "Entidade",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Entidade",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Entidade",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoVendedor",
                table: "Entidade",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoSistema",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "EmailTeste",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Gerente",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Hierarquia",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "TipoVendedor",
                table: "Entidade");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Entidade",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoSistema = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    EmailTeste = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });
        }
    }
}
