using Tivix.FamilyBudget.Server.Core.Categories.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Models;
public class Budget
{
    public Budget(string name, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Categories = new List<Category>();
        Name = name;
        OwnerId = ownerId;
    }

    public Budget(BudgetEntity budgetEntity)
    {
        Id = budgetEntity.Id;
        Categories = budgetEntity.Categories.Select(p => new Category(p)).ToList();
        Name = budgetEntity.Name;
        OwnerId = budgetEntity.OwnerId;
    }

    public BudgetEntity ToBudgetEntity() => new()
    {
        Id = Id,
        Name = Name,
        Categories = Categories.Select(p => p.ToCategoryEntity()).ToList()
    };

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; }

    public ICollection<Category> Categories { get; set; }
}
