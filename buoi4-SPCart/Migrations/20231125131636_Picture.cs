using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buoi4_SPCart.Migrations
{
    /// <inheritdoc />
    public partial class Picture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
