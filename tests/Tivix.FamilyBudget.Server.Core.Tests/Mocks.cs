using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests;
public class Mocks
{
    public Mocks()
    {
        UserGuid = Guid.NewGuid();
        CategoryGuid = Guid.NewGuid();
        BudgetGuid = Guid.NewGuid();
        FinancialEntryGuid = Guid.NewGuid();

    }

    public Guid UserGuid { get; private set; }
    public UserEntity UserEntity => new() { Id = UserGuid };

    public Guid CategoryGuid { get; private set; }
    public CategoryEntity CategoryEntity => new()
    {
        Id = CategoryGuid,
        Budget = BudgetEntity!,
        Name = "CategoryName"
    };

    public Guid BudgetGuid { get; private set; }
    public BudgetEntity BudgetEntity => new()
    {
        Id = BudgetGuid,
        Name = "BudgetName",
        User = UserEntity
    };

    public Guid FinancialEntryGuid { get; private set; }
    public FinancialEntryEntity FinancialEntryEntity => new()
    {
        Id = FinancialEntryGuid,
        Name = "FinancialEntryName",
        Category = CategoryEntity,
        IsExpense = true
    };

    public IUserProvider UserProviderMock = Substitute.For<IUserProvider>();

    public ApplicationContext GetApplicationContext()
    {
        return GetApplicationContext(Guid.NewGuid().ToString());
    }
    public ApplicationContext GetApplicationContext(string dbName)
    {
        return new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(dbName)
        .Options);
    }
}
