using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;

namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
public class FinancialEntriesQueriesTests : IClassFixture<ApplicationDataFixture>
{
    private readonly ApplicationDataFixture _fixture;

    public FinancialEntriesQueriesTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void GetFinancialEntriesByCategoryIdQueryHandler_GetsFinancialEntries_ForCorrectQuery()
    {
        var handler = new GetFinancialEntriesByCategoryIdQueryHandler(_fixture.FinancialEntriesQueries);
        _fixture.FinancialEntriesQueries.Categories.Add(Mocks.CategoryEntity);
        _fixture.FinancialEntriesQueries.FinancialEntries.Add(Mocks.FinancialEntryEntity);
        _fixture.FinancialEntriesQueries.SaveChanges();

        var result = await handler.Handle(new GetFinancialEntriesByCategoryIdQuery(Mocks.CategoryGuid), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(Mocks.FinancialEntryEntity.Id, result.First().Id);
        Assert.Equal(Mocks.FinancialEntryEntity.Name, result.First().Name);
    }
}
