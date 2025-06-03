using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class adicionadoReferenciaDoProdutoNoDepartamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoLojaId",
                table: "Departamento",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoLojaId",
                table: "Departamento");
        }
    }
}
