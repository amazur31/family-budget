using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;
using Tivix.FamilyBudget.Server.Core.Tests.Categories;

namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
public class FinancialEntriesQueriesTests
{
    Mocks Mocks { get; set; }
    public FinancialEntriesQueriesTests()
    {
        Mocks = new Mocks();
    }

    [Fact]
    public async void GetFinancialEntriesByCategoryIdQueryHandler_GetsFinancialEntries_ForCorrectQuery()
    {
        using var context = Mocks.GetApplicationContext();
        {
            var handler = new GetFinancialEntriesByCategoryIdQueryHandler(context);
            var category = Mocks.CategoryEntity;
            context.Categories.Add(category);
            var financialEntry = Mocks.FinancialEntryEntity;
            financialEntry.Category = category;
            context.FinancialEntries.Add(financialEntry);
            context.SaveChanges();

            var result = await handler.Handle(new GetFinancialEntriesByCategoryIdQuery(Mocks.CategoryGuid), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Mocks.FinancialEntryEntity.Id, result.First().Id);
            Assert.Equal(Mocks.FinancialEntryEntity.Name, result.First().Name);
        }
    }
}
