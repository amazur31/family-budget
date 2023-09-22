namespace Tivix.FamilyBudget.Server.Core.Tests.Categories;

[Collection("CategoriesTests")]
internal class CategoriesCommandsTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public CategoriesCommandsTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }
}
