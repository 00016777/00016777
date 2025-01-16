using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class basketschemachanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Drinks_DrinkId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Meals_MealId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "baskets",
                newSchema: "restaurant");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                newName: "basketItems",
                newSchema: "restaurant");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_UserId",
                schema: "restaurant",
                table: "baskets",
                newName: "IX_baskets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_MealId",
                schema: "restaurant",
                table: "basketItems",
                newName: "IX_basketItems_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_DrinkId",
                schema: "restaurant",
                table: "basketItems",
                newName: "IX_basketItems_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId",
                schema: "restaurant",
                table: "basketItems",
                newName: "IX_basketItems_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_baskets",
                schema: "restaurant",
                table: "baskets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_basketItems",
                schema: "restaurant",
                table: "basketItems",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_basketItems_Drinks_DrinkId",
                schema: "restaurant",
                table: "basketItems",
                column: "DrinkId",
                principalSchema: "restaurant",
                principalTable: "Drinks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_basketItems_Meals_MealId",
                schema: "restaurant",
                table: "basketItems",
                column: "MealId",
                principalSchema: "restaurant",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_basketItems_baskets_BasketId",
                schema: "restaurant",
                table: "basketItems",
                column: "BasketId",
                principalSchema: "restaurant",
                principalTable: "baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_baskets_AspNetUsers_UserId",
                schema: "restaurant",
                table: "baskets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basketItems_Drinks_DrinkId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_basketItems_Meals_MealId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_basketItems_baskets_BasketId",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_baskets_AspNetUsers_UserId",
                schema: "restaurant",
                table: "baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_baskets",
                schema: "restaurant",
                table: "baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_basketItems",
                schema: "restaurant",
                table: "basketItems");

            migrationBuilder.RenameTable(
                name: "baskets",
                schema: "restaurant",
                newName: "Baskets");

            migrationBuilder.RenameTable(
                name: "basketItems",
                schema: "restaurant",
                newName: "BasketItems");

            migrationBuilder.RenameIndex(
                name: "IX_baskets_UserId",
                table: "Baskets",
                newName: "IX_Baskets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_basketItems_MealId",
                table: "BasketItems",
                newName: "IX_BasketItems_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_basketItems_DrinkId",
                table: "BasketItems",
                newName: "IX_BasketItems_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_basketItems_BasketId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8214));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8216));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "e3b41b85-5321-4134-88d0-6f55c194f3e5", new DateTime(2024, 12, 26, 17, 34, 49, 270, DateTimeKind.Utc).AddTicks(8340), "AQAAAAIAAYagAAAAEDuKhw2ffEWcXMg8niGAOdJwHRJMt1d1ExyXNx+0EcZvE4PwsmLJH0JFNe/9pcDaHg==" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Drinks_DrinkId",
                table: "BasketItems",
                column: "DrinkId",
                principalSchema: "restaurant",
                principalTable: "Drinks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Meals_MealId",
                table: "BasketItems",
                column: "MealId",
                principalSchema: "restaurant",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
