using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaoClienteEParceiroEVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Entidade",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Entidade",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Contato",
                table: "Entidade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeioDeContato",
                table: "Entidade",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecebeEmail",
                table: "Entidade",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "Contato",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "MeioDeContato",
                table: "Entidade");

            migrationBuilder.DropColumn(
                name: "RecebeEmail",
                table: "Entidade");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Entidade",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
