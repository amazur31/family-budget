using Bogus;
using NSubstitute;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetById;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests;

public class BudgetsQueriesTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public BudgetsQueriesTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetBudgetByIdQueryHandler_GetsBudget_ForCorrectCommand()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.User);
        var handler = new GetBudgetByIdQueryHandler(_fixture.BudgetCommands);
        var budget = new Faker<BudgetEntity>().StrictMode(true)
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.Random.Words())
            .RuleFor(p => p.User, Mocks.User)
            .Generate();
        _fixture.BudgetCommands.Budgets.Add(budget);
        _fixture.BudgetCommands.SaveChanges();

        var result = await handler.Handle(new GetBudgetByIdQuery(budget.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(budget.Name, result.Name);
    }
}
