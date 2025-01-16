using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                schema: "restaurant",
                table: "OrderDetails",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                schema: "restaurant",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "restaurant",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "restaurant",
                table: "Images",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "restaurant",
                table: "basketItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PricePerKG = table.Column<decimal>(type: "numeric", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 211, DateTimeKind.Utc).AddTicks(497));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 211, DateTimeKind.Utc).AddTicks(528));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 211, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "3653d8b7-158e-443c-b80d-c153805223b6", new DateTime(2025, 1, 5, 16, 25, 17, 211, DateTimeKind.Utc).AddTicks(728), "AQAAAAIAAYagAAAAEGmQHVz9ikgdMqocX5NkMqZJX3Tf5s2X4ccvXAqedecbBJjRmTXYilZ+61rUmKxTaA==" });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3048));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3050));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3052));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3054));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2954));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2961));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2962));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2966));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2967));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2968));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2970));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 25, 17, 263, DateTimeKind.Utc).AddTicks(2971));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DrinkId",
                schema: "restaurant",
                table: "OrderDetails",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                schema: "restaurant",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                schema: "restaurant",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_basketItems_ProductId",
                schema: "restaurant",
                table: "basketItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_basketItems_Products_ProductId",
                schema: "restaurant",
                table: "basketItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                schema: "restaurant",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Drinks_DrinkId",
                schema: "restaurant",
                table: "OrderDetails",
                column: "DrinkId",
                principalSchema: "restaurant",
                principalTable: "Drinks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                schema: "restaurant",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basketItems_Products_ProductId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                schema: "restaurant",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Drinks_DrinkId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_DrinkId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                schema: "restaurant",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_basketItems_ProductId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "restaurant",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "restaurant",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                schema: "restaurant",
                table: "OrderDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 896, DateTimeKind.Utc).AddTicks(5892));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 896, DateTimeKind.Utc).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 896, DateTimeKind.Utc).AddTicks(5917));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "cbefc6b0-78ba-44b8-9055-e8b9654983ae", new DateTime(2024, 12, 29, 10, 42, 6, 896, DateTimeKind.Utc).AddTicks(6044), "AQAAAAIAAYagAAAAEPd+1ppKgWJ114X43OJI6hJaKmeUSOaoDLhHSsRcz0o+v/0xNauNkvn/cyYbRQvowQ==" });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2935));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2938));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2939));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2940));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2942));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2943));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2875));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2882));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2889));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2890));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2891));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2892));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 29, 10, 42, 6, 948, DateTimeKind.Utc).AddTicks(2893));
        }
    }
}
