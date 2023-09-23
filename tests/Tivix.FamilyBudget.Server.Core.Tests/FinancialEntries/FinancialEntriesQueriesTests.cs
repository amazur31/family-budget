using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;
using Tivix.FamilyBudget.Server.Core.Tests.Categories;

namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesQueriesTests")]
public class FinancialEntriesQueriesTests : IClassFixture<FinancialEntriesQueriesDataFixture>
{
    private readonly FinancialEntriesQueriesDataFixture _fixture;

    public FinancialEntriesQueriesTests(FinancialEntriesQueriesDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetFinancialEntriesByCategoryIdQueryHandler_GetsFinancialEntries_ForCorrectQuery()
    {
        using var context = _fixture.Context;
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
