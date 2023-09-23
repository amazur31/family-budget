using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.CreateFinancialEntryCommand;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.DeleteFinancialEntryCommand;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.UpdateFinancialEntryCommand;

namespace Tivix.FamilyBudget.Server.Core.Tests.FinancialEntries;

[Collection("FinancialEntriesTests")]
public class FinancialEntriesCommandsTests : IClassFixture<ApplicationDataFixture>
{
    ApplicationDataFixture _fixture;

    public FinancialEntriesCommandsTests(ApplicationDataFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void CreateFinancialEntryCommandHandler_AddsFinancialEntry_ForCorrectCommand()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
        var handler = new CreateFinancialEntryCommandHandler(_fixture.FinancialEntriesCommands);
        _fixture.FinancialEntriesCommands.Categories.Add(Mocks.CategoryEntity);
        _fixture.FinancialEntriesCommands.SaveChanges();

        var result = await handler.Handle(new CreateFinancialEntryCommand("SomeName", true, Mocks.CategoryGuid), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("SomeName", result.Name);
        Assert.NotNull(_fixture.FinancialEntriesCommands.FinancialEntries.Single(p => p.Id == result.Id));
    }

    [Fact]
    public async void DeleteFinancialEntryCommandHandler_DeletesFinancialEntry_ForCorrectCommand()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
        var handler = new DeleteFinancialEntryCommandHandler(_fixture.FinancialEntriesCommands);
        _fixture.FinancialEntriesCommands.FinancialEntries.Add(Mocks.FinancialEntryEntity);
        _fixture.FinancialEntriesCommands.SaveChanges();

        Assert.NotNull(_fixture.FinancialEntriesCommands.FinancialEntries.SingleOrDefault(p => p.Id == Mocks.FinancialEntryGuid));

        await handler.Handle(new DeleteFinancialEntryCommand(Mocks.FinancialEntryGuid), CancellationToken.None);

        Assert.Null(_fixture.FinancialEntriesCommands.FinancialEntries.SingleOrDefault(p => p.Id == Mocks.FinancialEntryGuid));
    }

    [Fact]
    public async void UpdateFinancialEntryCommandHandler_UpdatesFinancialEntry_ForCorrectCommand()
    {
        Mocks.UserProviderMock.UserEntity.Returns(Mocks.UserEntity);
        var handler = new UpdateFinancialEntryCommandHandler(_fixture.FinancialEntriesCommands);
        _fixture.FinancialEntriesCommands.FinancialEntries.Add(Mocks.FinancialEntryEntity);
        var newCategory = Mocks.CategoryEntity;
        newCategory.Id = Guid.NewGuid();
        _fixture.FinancialEntriesCommands.Categories.Add(newCategory);
        _fixture.FinancialEntriesCommands.SaveChanges();

        var result = await handler.Handle(new UpdateFinancialEntryCommand(Mocks.FinancialEntryGuid, "SomeName2", false, newCategory.Id), CancellationToken.None); ;

        Assert.NotNull(result);
        Assert.Equal("SomeName2", result.Name);
        Assert.False(result.IsExpense);
        Assert.Equal(newCategory.Id, result.CategoryId);
        Assert.NotNull(_fixture.FinancialEntriesCommands.FinancialEntries.Single(p => p.Id == result.Id));
    }
}
