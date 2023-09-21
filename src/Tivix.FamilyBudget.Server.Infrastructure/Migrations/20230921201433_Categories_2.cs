using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.FamilyBudget.Server.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Categories_2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CategoryEntity");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CategoryEntity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BudgetEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                Name = table.Column<string>(type: "text", nullable: false)
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
}
