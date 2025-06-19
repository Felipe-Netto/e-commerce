using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTokenFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
