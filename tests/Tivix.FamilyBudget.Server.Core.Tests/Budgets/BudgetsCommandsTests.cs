using NSubstitute;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

namespace Tivix.FamilyBudget.Server.Core.Tests.Budgets;

public class BudgetsCommandsTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public BudgetsCommandsTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetBudgetByIdQuery_ReturnsBudget_ForCorrectQuery()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.User);
        var handler = new CreateBudgetCommandHandler(_fixture.BudgetCommands, Mocks.UserProviderMock);

        var result = await handler.Handle(new CreateBudgetCommand("SomeName"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("SomeName", result.Name);
        Assert.NotNull(_fixture.BudgetCommands.Budgets.Single(p => p.Id == result.Id));
    }
}
