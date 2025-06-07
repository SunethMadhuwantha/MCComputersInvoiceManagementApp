using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCComputers.InvoiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "PaidAmount",
                table: "Invoices",
                newName: "Discount");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Invoices",
                newName: "PaidAmount");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
