using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Tests;
public class ApplicationDataFixture : IDisposable
{
    public ApplicationContext BudgetsCommands { get; private set; }
    public ApplicationContext BudgetsQueries { get; private set; }
    public ApplicationContext CategoriesCommands { get; private set; }
    public ApplicationContext CategoriesQueries { get; private set; }
    public ApplicationContext FinancialEntriesCommands { get; private set; }
    public ApplicationContext FinancialEntriesQueries { get; private set; }


    public ApplicationDataFixture()
    {
        BudgetsCommands = GetApplicationContext(nameof(BudgetsCommands));
        BudgetsQueries = GetApplicationContext(nameof(BudgetsQueries));
        CategoriesCommands = GetApplicationContext(nameof(CategoriesCommands));
        CategoriesQueries = GetApplicationContext(nameof(CategoriesQueries));
        FinancialEntriesCommands = GetApplicationContext(nameof(FinancialEntriesCommands));
        FinancialEntriesQueries = GetApplicationContext(nameof(FinancialEntriesQueries));

        ApplicationContext GetApplicationContext(string dbName)
        {
            return new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(dbName)
            .Options);
        }
    }

    public void Dispose()
    {
        BudgetsCommands.Dispose();
    }
}
