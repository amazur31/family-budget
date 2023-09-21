using Tivix.FamilyBudget.Server.Core.Categories.Models;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;
public class BudgetEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Category> Categories {get; set;} = null!;
}
