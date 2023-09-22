using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.FamilyBudget.Server.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Misc : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Categories_AspNetUsers_UserId1",
            table: "Categories");

        migrationBuilder.DropIndex(
            name: "IX_Categories_UserId1",
            table: "Categories");

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "90db1d78-c639-42c1-b6a8-b741975d3322");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Categories");

        migrationBuilder.DropColumn(
            name: "UserId1",
            table: "Categories");

        migrationBuilder.RenameColumn(
            name: "OwnerId",
            table: "Budgets",
            newName: "UserId");

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "BudgetsAccessible", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            values: new object[] { "326f25c1-347a-4521-8cde-572ce017755d", 0, null, "69858c8d-04bd-4e41-a7b1-f5c738e01975", "example@example.com", true, false, null, "EXAMPLE@EXAMPLE.COM", "EXAMPLE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ1yKBk/2UNYl6o16aimTV6gUrwQlKHGRcLbO14Oam8e31VG7YxHgEOdWau4AoaXNw==", "1234567890", true, "HE2MFWR5BLKWMUN3KVXTVSMILHRWTQYD", false, "example@example.com" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "326f25c1-347a-4521-8cde-572ce017755d");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "Budgets",
            newName: "OwnerId");

        migrationBuilder.AddColumn<Guid>(
            name: "UserId",
            table: "Categories",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "UserId1",
            table: "Categories",
            type: "text",
            nullable: true);

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "BudgetsAccessible", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            values: new object[] { "90db1d78-c639-42c1-b6a8-b741975d3322", 0, null, "5bfec93d-a286-4e0e-a79b-f1193a612d14", "example@example.com", true, false, null, "EXAMPLE@EXAMPLE.COM", "EXAMPLE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ1yKBk/2UNYl6o16aimTV6gUrwQlKHGRcLbO14Oam8e31VG7YxHgEOdWau4AoaXNw==", "1234567890", true, "HE2MFWR5BLKWMUN3KVXTVSMILHRWTQYD", false, "example@example.com" });

        migrationBuilder.CreateIndex(
            name: "IX_Categories_UserId1",
            table: "Categories",
            column: "UserId1");

        migrationBuilder.AddForeignKey(
            name: "FK_Categories_AspNetUsers_UserId1",
            table: "Categories",
            column: "UserId1",
            principalTable: "AspNetUsers",
            principalColumn: "Id");
    }
}
