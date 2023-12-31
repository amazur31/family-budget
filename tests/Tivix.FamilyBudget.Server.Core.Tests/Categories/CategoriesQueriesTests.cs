﻿using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetByIdQuery;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesTests")]
public class CategoriesQueriesTests
{
    Mocks Mocks { get; set; }
    public CategoriesQueriesTests()
    {
        Mocks = new Mocks();
    }

    [Fact]
    public async void GetCategoriesByBudgetIdQueryHandler_GetsCategory_ForCorrectQuery()
    {
        using var context = Mocks.GetApplicationContext();
        {
            Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
            var handler = new GetCategoriesByBudgetIdQueryHandler(context);
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
