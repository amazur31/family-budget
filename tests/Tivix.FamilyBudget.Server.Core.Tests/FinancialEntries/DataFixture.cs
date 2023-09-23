using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;
public class FinancialEntriesCommandsDataFixture : IDisposable
{
    public ApplicationContext Context { get { return GetApplicationContext(Guid.NewGuid().ToString()); } }

    static ApplicationContext GetApplicationContext(string dbName)
    {
        return new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(dbName)
        .Options);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}

public class FinancialEntriesQueriesDataFixture : IDisposable
{
    public ApplicationContext Context { get { return GetApplicationContext(Guid.NewGuid().ToString()); } }

    static ApplicationContext GetApplicationContext(string dbName)
    {
        return new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(dbName)
        .Options);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
