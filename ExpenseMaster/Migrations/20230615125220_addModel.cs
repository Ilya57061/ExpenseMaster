using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseMaster.Migrations
{
    /// <inheritdoc />
    public partial class addModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expenses",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Category 1" },
                    { 2, "Category 2" },
                    { 3, "Category 3" },
                    { 4, "Category 4" },
                    { 5, "Category 5" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "UserId" },
                values: new object[,]
                {
                    { 1, 100.00m, 1, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2975), 1 },
                    { 2, 50.00m, 2, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2987), 1 },
                    { 3, 75.00m, 1, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2988), 2 },
                    { 4, 120.00m, 3, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2989), 2 },
                    { 5, 200.00m, 2, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2990), 3 }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "UserId" },
                values: new object[,]
                {
                    { 1, 1000.00m, 1, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3003), 1 },
                    { 2, 750.00m, 2, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3004), 1 },
                    { 3, 500.00m, 1, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3005), 2 },
                    { 4, 1200.00m, 3, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3006), 2 },
                    { 5, 800.00m, 2, new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3006), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expenses",
                type: "decimal(10,2",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
