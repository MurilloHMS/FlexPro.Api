using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncludeInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "system_code",
                table: "inventory_movements");

            migrationBuilder.AddColumn<int>(
                name: "inventory_product_id",
                table: "inventory_movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_inventory_product_id",
                table: "inventory_movements",
                column: "inventory_product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_inventory_products_inventory_product_id",
                table: "inventory_movements",
                column: "inventory_product_id",
                principalTable: "inventory_products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_movements_inventory_products_inventory_product_id",
                table: "inventory_movements");

            migrationBuilder.DropIndex(
                name: "IX_inventory_movements_inventory_product_id",
                table: "inventory_movements");

            migrationBuilder.DropColumn(
                name: "inventory_product_id",
                table: "inventory_movements");

            migrationBuilder.AddColumn<string>(
                name: "TempId",
                table: "inventory_products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "system_code",
                table: "inventory_movements",
                type: "varchar",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_inventory_products_TempId",
                table: "inventory_products",
                column: "TempId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_system_code",
                table: "inventory_movements",
                column: "system_code");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_movements_inventory_products_system_code",
                table: "inventory_movements",
                column: "system_code",
                principalTable: "inventory_products",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
