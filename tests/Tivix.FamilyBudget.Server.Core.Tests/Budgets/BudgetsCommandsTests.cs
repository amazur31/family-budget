﻿using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.ShareBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.UnshareBudgetCommandHandler;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

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
        using var context = _fixture.Context;
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
    public async void ShareBudgetCommandHandler_SharesBudget_ForCorrectCommand()
    {
        using var context = _fixture.Context;
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
        using var context = _fixture.Context;
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
