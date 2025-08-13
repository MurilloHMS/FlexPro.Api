using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncludeInventoryMovementAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_system_code",
                table: "inventory_movements",
                column: "system_code");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_inventory_products_system_code",
                table: "inventory_movements",
                column: "system_code",
                principalTable: "inventory_products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movements_inventory_products_system_code",
                table: "inventory_movements");

            migrationBuilder.DropIndex(
                name: "IX_inventory_movements_system_code",
                table: "inventory_movements");
        }
    }
}
