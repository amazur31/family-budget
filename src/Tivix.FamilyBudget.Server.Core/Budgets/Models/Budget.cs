using Tivix.FamilyBudget.Server.Core.Categories.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Models;
public class Budget
{
    public Budget(string name, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Name = name;
        OwnerId = ownerId;
    }

    public Budget(BudgetEntity budgetEntity)
    {
        Id = budgetEntity.Id;
        Name = budgetEntity.Name;
        OwnerId = budgetEntity.OwnerId;
    }

    public BudgetEntity ToBudgetEntity() => new()
    {
        Id = Id,
        Name = Name,
    };

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; }
}
