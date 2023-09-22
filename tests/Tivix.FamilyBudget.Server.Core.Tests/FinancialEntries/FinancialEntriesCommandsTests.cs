namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
internal class FinancialEntriesCommandsTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public FinancialEntriesCommandsTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }
}
