using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddEf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DddEfMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tm_Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tm_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tm_Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tm_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder_Item",
                columns: table => new
                {
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    Id1 = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Det1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder_Item", x => new { x.Id1, x.RowNumber });
                    table.ForeignKey(
                        name: "FK_Tx_SalesOrder_Item_Tx_SalesOrder_Id",
                        column: x => x.Id,
                        principalTable: "Tx_SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tx_SalesOrder_Item_Id",
                table: "Tx_SalesOrder_Item",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tm_Customer");

            migrationBuilder.DropTable(
                name: "Tm_Product");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder_Item");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder");
        }
    }
}
