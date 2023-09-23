using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetById;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests;

[Collection("BudgetsTests")]
public class BudgetsQueriesTests : IClassFixture<ApplicationDataFixture>
{
    readonly ApplicationDataFixture _fixture;

    public BudgetsQueriesTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetBudgetByIdQueryHandler_GetsBudget_ForCorrectQuery()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
        var handler = new GetBudgetByIdQueryHandler(_fixture.BudgetsQueries);
        var budget = new Faker<BudgetEntity>().StrictMode(true)
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.Random.Words())
            .RuleFor(p => p.User, Mocks.UserEntity)
            .Generate();
        _fixture.BudgetsQueries.Budgets.Add(budget);
        _fixture.BudgetsQueries.SaveChanges();

        var result = await handler.Handle(new GetBudgetByIdQuery(budget.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(budget.Name, result.Name);
    }
}
