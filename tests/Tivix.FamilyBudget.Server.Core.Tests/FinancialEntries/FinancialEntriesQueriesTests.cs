using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;
using Tivix.FamilyBudget.Server.Core.Tests.Categories;

namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
public class FinancialEntriesQueriesTests
{

    [Fact]
    public async void GetFinancialEntriesByCategoryIdQueryHandler_GetsFinancialEntries_ForCorrectQuery()
    {
        using var context = Mocks.GetApplicationContext();
        {
            var handler = new GetFinancialEntriesByCategoryIdQueryHandler(context);
            context.Categories.Add(Mocks.CategoryEntity);
            context.FinancialEntries.Add(Mocks.FinancialEntryEntity);
            context.SaveChanges();

            var result = await handler.Handle(new GetFinancialEntriesByCategoryIdQuery(Mocks.CategoryGuid), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Mocks.FinancialEntryEntity.Id, result.First().Id);
            Assert.Equal(Mocks.FinancialEntryEntity.Name, result.First().Name);
        }
    }
}
