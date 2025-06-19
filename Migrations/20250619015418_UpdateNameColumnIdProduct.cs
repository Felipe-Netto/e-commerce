using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameColumnIdProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_product",
                table: "Products",
                newName: "idProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "Products",
                newName: "id_product");
        }
    }
}
