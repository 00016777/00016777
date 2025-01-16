using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userandrolechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "CreatedDate", "UpdatedDate" },
                values: new object[] { true, new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8188), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Active", "CreatedDate", "UpdatedDate" },
                values: new object[] { true, new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8214), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Active", "CreatedDate", "UpdatedDate" },
                values: new object[] { true, new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8216), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { true, "e3b41b85-5321-4134-88d0-6f55c194f3e5", new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8340), "AQAAAAIAAYagAAAAEDuKhw2ffEWcXMg8niGAOdJwHRJMt1d1ExyXNx+0EcZvE4PwsmLJH0JFNe/9pcDaHg==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5464));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5465));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5467));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5469));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5395));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5403));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5405));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5411));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5413));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5414));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 337, DateTimeKind.Utc).AddTicks(5415));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d44e766-a6b9-4c94-88af-376f0c71c393", "AQAAAAIAAYagAAAAEHVcwtDDNIDGjKM69DeZ54NYpgA7tvxC/Eaq0gbITU1UA/0WZiB/xX0XejutWZJWuw==" });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5959));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5962));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5963));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5964));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5965));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5967));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5894));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5900));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5907));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5908));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5910));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 18, 32, 9, 89, DateTimeKind.Utc).AddTicks(5911));
        }
    }
}
