namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesTests")]
internal class CategoriesQueriesTests : IClassFixture<CategoriesQueriesDataFixture>
{
    private readonly CategoriesQueriesDataFixture _fixture;

    public CategoriesQueriesTests(CategoriesQueriesDataFixture fixture)
    {
        _fixture = fixture;
    }
}
