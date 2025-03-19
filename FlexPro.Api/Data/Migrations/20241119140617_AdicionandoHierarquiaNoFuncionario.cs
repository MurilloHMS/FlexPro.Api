using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoHierarquiaNoFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hierarquia",
                table: "Funcionarios",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hierarquia",
                table: "Funcionarios");
        }
    }
}
