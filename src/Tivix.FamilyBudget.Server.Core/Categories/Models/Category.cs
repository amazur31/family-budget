namespace Tivix.FamilyBudget.Server.Core.Categories.Models;
public class Category
{
    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; set; }
}
