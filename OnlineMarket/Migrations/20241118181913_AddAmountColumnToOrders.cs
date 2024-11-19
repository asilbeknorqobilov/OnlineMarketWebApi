using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMarket.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountColumnToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Orders");
        }
    }
}
