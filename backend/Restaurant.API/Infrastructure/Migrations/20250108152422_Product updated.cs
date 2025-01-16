using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Productupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "restaurant");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 781, DateTimeKind.Utc).AddTicks(9566));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 781, DateTimeKind.Utc).AddTicks(9596));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 781, DateTimeKind.Utc).AddTicks(9639));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "280202da-1107-4119-8a18-17a8735dfc1a", new DateTime(2025, 1, 8, 15, 24, 21, 781, DateTimeKind.Utc).AddTicks(9981), "AQAAAAIAAYagAAAAEJBuNgfQCXn+ZCa6Ca5hTR9Mh6UNtYhjP4MN1P5uvUWPowaPFDU86rZwogsoy0mWlw==" });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1817));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1819));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1820));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1822));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1824));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1825));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1758));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1760));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1761));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 15, 24, 21, 833, DateTimeKind.Utc).AddTicks(1763));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                schema: "restaurant",
                newName: "Products");

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
        }
    }
}
