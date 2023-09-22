namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class CategoryEntity
{
    public Guid Id { get; set; }
    public BudgetEntity Budget { get; set; } = null!;
    public string Name { get; set; } = null!;
}
