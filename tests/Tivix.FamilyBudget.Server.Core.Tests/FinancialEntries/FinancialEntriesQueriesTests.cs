namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
internal class FinancialEntriesQueriesTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public FinancialEntriesQueriesTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }
}
