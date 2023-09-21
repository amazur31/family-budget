using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Models;
public class Category
{
    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Category(CategoryEntity categoryEntity)
    {
        Id = categoryEntity.Id;
        Name = categoryEntity.Name;
    }

    public CategoryEntity ToCategoryEntity()
    {
        return new() { Id = Id, Name = Name };
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
