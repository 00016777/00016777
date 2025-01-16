using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class basketandbasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BasketId = table.Column<int>(type: "integer", nullable: false),
                    MealId = table.Column<int>(type: "integer", nullable: true),
                    DrinkId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalSchema: "restaurant",
                        principalTable: "Drinks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasketItems_Meals_MealId",
                        column: x => x.MealId,
                        principalSchema: "restaurant",
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_DrinkId",
                table: "BasketItems",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_MealId",
                table: "BasketItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4d776bcd-7783-42b8-8bae-9bf284fb5594", "AQAAAAIAAYagAAAAEJWxr6/PCYFKFVaXhN/eoKEmqKLTABKwkTTE3qzrntvr95T5p0Gi5DvDd6CaWW4gpw==" });

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9976));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9977));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9981));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9916));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9923));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9925));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9930));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9932));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9933));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9934));

            migrationBuilder.UpdateData(
                schema: "restaurant",
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 25, 17, 44, 15, 489, DateTimeKind.Utc).AddTicks(9935));
        }
    }
}
