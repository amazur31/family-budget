using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.DeleteBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.ShareBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.UnshareBudgetCommandHandler;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.UpdateBudgetCommand;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests.Budgets;

[Collection("BudgetsTests")]
public class BudgetsCommandsTests
{
    [Fact]
    public async void CreateBudgetCommandHandler_AddsBudget_ForCorrectCommand()
    {
        using var context = Mocks.GetApplicationContext();
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            var handler = new CreateBudgetCommandHandler(context, Mocks.UserProviderMock);

            var result = await handler.Handle(new CreateBudgetCommand("SomeName"), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("SomeName", result.Name);
            Assert.NotNull(context.Budgets.Single(p => p.Id == result.Id));
        }
    }

    [Fact]
    public async void UpdateBudgetCommandHandler_UpdatesBudget_ForCorrectCommand()
    {
        using var context = Mocks.GetApplicationContext();
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();
            var handler = new UpdateBudgetCommandHandler(context, Mocks.UserProviderMock);

            var result = await handler.Handle(new UpdateBudgetCommand(Mocks.BudgetGuid, "SomeName2"), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("SomeName2", result.Name);
            Assert.NotNull(context.Budgets.Single(p => p.Id == result.Id));
        }
    }

    [Fact]
    public async void DeleteBudgetCommandHandler_DeletesBudget_ForCorrectCommand()
    {
        using var context = Mocks.GetApplicationContext();
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();
            var handler = new DeleteBudgetCommandHandler(context, Mocks.UserProviderMock);

            await handler.Handle(new DeleteBudgetCommand(Mocks.BudgetGuid), CancellationToken.None);

            Assert.Null(context.Budgets.SingleOrDefault(p => p.Id == Mocks.BudgetGuid));
        }
    }

    [Fact]
    public async void ShareBudgetCommandHandler_SharesBudget_ForCorrectCommand()
    {
        using var context = Mocks.GetApplicationContext();
        {
            var handler = new ShareBudgetCommandHandler(context);
            var userWithAccess = new UserEntity() { Id = Guid.NewGuid() };
            context.Users.Add(userWithAccess);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();

            await handler.Handle(new ShareBudgetCommand(Mocks.BudgetGuid, userWithAccess.Id), CancellationToken.None);
            var resultUser = context.Users.SingleOrDefault(p => p.Id == userWithAccess.Id);

            Assert.NotNull(resultUser!.BudgetsAccessible);
            Assert.Equal(Mocks.BudgetEntity.Id, resultUser!.BudgetsAccessible.First());
        }
    }

    [Fact]
    public async void UnshareBudgetCommandHandler_UnsharesBudget_ForCorrectCommand()
    {
        using var context = Mocks.GetApplicationContext();
        {
            var user = new UserEntity() { Id = Guid.NewGuid(), BudgetsAccessible = new List<Guid>() { Mocks.BudgetGuid } };
            Mocks.UserProviderMock.UserEntity.Returns(user);
            var handler = new UnshareBudgetCommandHandler(context);
            context.Users.Add(user);
            context.Budgets.Add(Mocks.BudgetEntity);
            context.SaveChanges();

            await handler.Handle(new UnshareBudgetCommand(Mocks.BudgetGuid, user.Id), CancellationToken.None);
            var resultUser = context.Users.SingleOrDefault(p => p.Id == user.Id);

            Assert.NotNull(resultUser!.BudgetsAccessible);
            Assert.Empty(resultUser!.BudgetsAccessible);
        }
    }
}
