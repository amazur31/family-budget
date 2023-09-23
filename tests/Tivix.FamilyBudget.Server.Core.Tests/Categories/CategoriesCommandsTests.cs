using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.UpdateCategoryCommand;

namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesTests")]
public class CategoriesCommandsTests : IClassFixture<CategoriesCommandsDataFixture>
{
    private readonly CategoriesCommandsDataFixture _fixture;

    public CategoriesCommandsTests(CategoriesCommandsDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void CreateCategoriesCommandHandler_AddsCategory_ForCorrectCommand()
    {
        using var context = _fixture.Context;
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            context.Budgets.Add(Mocks.BudgetEntity);
            var handler = new CreateCategoryCommandHandler(context);

            var result = await handler.Handle(new CreateCategoryCommand("SomeName", Mocks.BudgetGuid), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("SomeName", result.Name);
            Assert.NotNull(context.Categories.Single(p => p.Id == result.Id));
        }
    }

    [Fact]
    public async void UpdateCategoriesCommandHandler_UpdatesCategory_ForCorrectCommand()
    {
        using var context = _fixture.Context;
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            context.Categories.Add(Mocks.CategoryEntity);
            context.SaveChanges();
            var handler = new UpdateCategoryCommandHandler(context);

            var result = await handler.Handle(new UpdateCategoryCommand(Mocks.CategoryEntity.Id, "SomeName2"), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("SomeName2", result.Name);
            Assert.NotNull(context.Categories.Single(p => p.Id == result.Id));
        }
    }

    [Fact]
    public async void DeleteCategoriesCommandHandler_DeletesCategory_ForCorrectCommand()
    {
        using var context = _fixture.Context;
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            context.Categories.Add(Mocks.CategoryEntity);
            context.SaveChanges();
            var handler = new DeleteCategoryCommandHandler(context);

            await handler.Handle(new DeleteCategoryCommand(Mocks.CategoryGuid), CancellationToken.None);

            Assert.Null(context.Categories.SingleOrDefault(p => p.Id == Mocks.CategoryGuid));
        }
    }
}
