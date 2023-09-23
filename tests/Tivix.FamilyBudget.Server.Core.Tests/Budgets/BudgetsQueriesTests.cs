using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetByIdQuery;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetSharedBudgets;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests.Budgets;

[Collection("BudgetsTests")]
public class BudgetsQueriesTests
{
    Mocks Mocks { get; set; }
    public BudgetsQueriesTests()
    {
        Mocks = new Mocks();
    }

    [Fact]
    public async void GetBudgetByIdQueryHandler_GetsBudget_ForCorrectQuery()
    {
        using var context = Mocks.GetApplicationContext();
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            var handler = new GetBudgetByIdQueryHandler(context);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();

            var result = await handler.Handle(new GetBudgetByIdQuery(Mocks.BudgetGuid), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(Mocks.BudgetEntity.Name, result.Name);
        }
    }

    [Fact]
    public async void GetSharedBudgetsQuery_GetsBudget_ForCorrectQuery()
    {
        using var context = Mocks.GetApplicationContext();
        {
            var user = new UserEntity() { Id = Guid.NewGuid(), BudgetsAccessible = new List<Guid>() { Mocks.BudgetGuid } };
            Mocks.UserProviderMock.UserEntity.Returns(user);
            var handler = new GetSharedBudgetsQueryHandler(context, Mocks.UserProviderMock);
            context.Users.Add(user);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();

            var result = await handler.Handle(new GetSharedBudgetsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result.Budgets);
            Assert.Equal(Mocks.BudgetEntity.Id, result.Budgets.First().Id);
        }
    }
}
