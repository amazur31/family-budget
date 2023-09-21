using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.FamilyBudget.Server.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Categories : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Category");

        migrationBuilder.AddColumn<Guid>(
            name: "OwnerId",
            table: "Budgets",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateTable(
            name: "CategoryEntity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                BudgetEntityId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                table.ForeignKey(
                    name: "FK_CategoryEntity_Budgets_BudgetEntityId",
                    column: x => x.BudgetEntityId,
                    principalTable: "Budgets",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_CategoryEntity_BudgetEntityId",
            table: "CategoryEntity",
            column: "BudgetEntityId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CategoryEntity");

        migrationBuilder.DropColumn(
            name: "OwnerId",
            table: "Budgets");

        migrationBuilder.CreateTable(
            name: "Category",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BudgetEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Category", x => x.Id);
                table.ForeignKey(
                    name: "FK_Category_Budgets_BudgetEntityId",
                    column: x => x.BudgetEntityId,
                    principalTable: "Budgets",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Category_BudgetEntityId",
            table: "Category",
            column: "BudgetEntityId");
    }
}
