namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class FinancialEntryEntity
{
    public Guid Id { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsExpense { get; set; }
}
