using Tivix.FamilyBudget.Server.Core.Categories.Models;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Models;
public class Budget
{
    public Budget()
    {
        Id = Guid.NewGuid();
        Categories = new List<Category>();
    }

    public Guid Id { get; set; }

    public ICollection<Category> Categories { get; set; }
}
