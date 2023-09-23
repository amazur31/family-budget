namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesQueriesTests")]
internal class CategoriesQueriesTests : IClassFixture<ApplicationDataFixture>
{
    private readonly ApplicationDataFixture _fixture;

    public CategoriesQueriesTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }
}
