using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetByIdQuery;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesTests")]
public class CategoriesQueriesTests : IClassFixture<CategoriesQueriesDataFixture>
{
    private readonly CategoriesQueriesDataFixture _fixture;

    public CategoriesQueriesTests(CategoriesQueriesDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetCategoriesByBudgetIdQueryHandler_GetsCategory_ForCorrectQuery()
    {
        using var context = _fixture.Context;
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            var handler = new GetCategoriesByBudgetIdQueryHandler(context);
            context.Budgets.Add(Mocks.BudgetEntity);
            var category = Mocks.CategoryEntity;
            category.Budget = Mocks.BudgetEntity;
            context.Categories.Add(category);
            context.SaveChanges();

            var result = await handler.Handle(new GetCategoriesByBudgetIdQuery(Mocks.BudgetGuid), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(category.Name, result.First().Name);
            Assert.Equal(category.Id, result.First().Id);
        }
    }
}
