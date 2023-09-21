using MediatR;
using Tivix.FamilyBudget.Server.Core.Categories.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
public record CreateCategoryCommand(string Name, Guid BudgetId) : IRequest<Category>;
internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    ApplicationContext _context;
    public CreateCategoryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.BudgetId);

        var result = await _context.Categories.AddAsync(category.ToCategoryEntity());
        _context.SaveChanges();

        return new(result.Entity);
    }
}
