using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appv1.Migrations
{
    public partial class ShoppingCart_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Admin",
                table: "Login",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Login");
        }
    }
}
