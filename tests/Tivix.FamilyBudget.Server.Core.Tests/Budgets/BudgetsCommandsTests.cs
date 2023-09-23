using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

namespace Tivix.FamilyBudget.Server.Core.Tests.Budgets;

[Collection("BudgetsTests")]
public class BudgetsCommandsTests : IClassFixture<ApplicationDataFixture>
{
    private readonly ApplicationDataFixture _fixture;

    public BudgetsCommandsTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void CreateBudgetCommandHandler_AddsBudget_ForCorrectCommand()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
        var handler = new CreateBudgetCommandHandler(_fixture.BudgetsCommands, Mocks.UserProviderMock);

        var result = await handler.Handle(new CreateBudgetCommand("SomeName"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("SomeName", result.Name);
        Assert.NotNull(_fixture.BudgetsCommands.Budgets.Single(p => p.Id == result.Id));
    }
}
