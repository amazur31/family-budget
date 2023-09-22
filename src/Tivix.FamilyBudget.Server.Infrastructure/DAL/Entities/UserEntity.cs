using Microsoft.AspNetCore.Identity;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class UserEntity : IdentityUser
{
    public List<BudgetEntity> Budgets { get; set; } = new List<BudgetEntity>();
    public List<Guid>? BudgetsAccessible { get; set; }

}
