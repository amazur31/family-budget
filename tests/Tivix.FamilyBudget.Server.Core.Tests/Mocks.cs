using NSubstitute;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests;
public static class Mocks
{
    public static Guid UserGuid => Guid.Parse("d5622414-4757-4023-b24c-f8ae19af0747");
    public static readonly UserEntity UserEntity = new() { Id = UserGuid };

    public static Guid CategoryGuid => Guid.Parse("8408a432-19a9-4f25-a494-f6ddd1ae0db4");
    public static readonly CategoryEntity CategoryEntity = new()
    {
        Id = CategoryGuid,
        Budget = BudgetEntity!,
        Name = "CategoryName"
    };

    public static Guid BudgetGuid => Guid.Parse("5fefccda-dd0c-4370-ab87-52b04fe28ef8");
    public static readonly BudgetEntity BudgetEntity = new()
    {
        Id = BudgetGuid,
        Name = "BudgetName",
        User = UserEntity
    };

    public static Guid FinancialEntryGuid => Guid.Parse("46509b58-9a00-48b3-9a63-cd7c27bbf138");
    public static readonly FinancialEntryEntity FinancialEntryEntity = new()
    {
        Id = FinancialEntryGuid,
        Name = "FinancialEntryName",
        Category = CategoryEntity,
        IsExpense = true
    };

    public static IUserProvider UserProviderMock = Substitute.For<IUserProvider>();
}
