namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class BudgetEntity
{
    public Guid Id { get; set; }
    public UserEntity User { get; set; } = null!;
    public string Name { get; set; } = null!;
}
