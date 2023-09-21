using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Models;
public class Category
{
    public Category(string name, Guid budgetId)
    {
        Id = Guid.NewGuid();
        BudgetId = budgetId;
        Name = name;
    }

    public Category(CategoryEntity categoryEntity)
    {
        Id = categoryEntity.Id;
        BudgetId = categoryEntity.BudgetId;
        Name = categoryEntity.Name;
    }

    public CategoryEntity ToCategoryEntity()
    {
        return new() { Id = Id, BudgetId = BudgetId, Name = Name };
    }

    public Guid Id { get; private set; }
    public Guid BudgetId { get; private set; }
    public string Name { get; private set; }
}
