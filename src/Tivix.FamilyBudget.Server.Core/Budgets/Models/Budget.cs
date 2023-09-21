namespace Tivix.FamilyBudget.Server.Core.Budgets.Models;
public class Budget
{
    public Budget()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}
