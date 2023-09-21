namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class BudgetEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = null!;
}
