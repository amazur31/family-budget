namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class CategoryEntity
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public string Name { get; set; } = null!;
}
