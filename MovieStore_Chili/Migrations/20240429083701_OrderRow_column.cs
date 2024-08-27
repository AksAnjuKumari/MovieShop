using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStore_Chili.Migrations
{
    /// <inheritdoc />
    public partial class OrderRow_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderRows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderRows");
        }
    }
}
