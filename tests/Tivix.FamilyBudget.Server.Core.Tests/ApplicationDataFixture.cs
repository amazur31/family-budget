using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Tests;
public class ApplicationDataFixture : IDisposable
{
    public ApplicationContext BudgetCommands { get; private set; }

    public ApplicationDataFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase("BudgetCommands")
            .Options;

        BudgetCommands = new ApplicationContext(options);
    }

    public void Dispose()
    {
        BudgetCommands.Dispose();
    }
}
