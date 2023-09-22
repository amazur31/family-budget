using MediatR;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
public record CreateCategoryCommand(string Name, Guid BudgetId) : IRequest<CreateCategoryResponse>;
public record CreateCategoryResponse(Guid Id, string Name);

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    ApplicationContext _context;
    public CreateCategoryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var budget = _context.Budgets.FirstOrDefault(p => p.Id == request.BudgetId);
        var categoryEntity = new CategoryEntity() { Budget = budget!, Id = Guid.NewGuid(), Name = request.Name };
        var result = await _context.Categories.AddAsync(categoryEntity);
        _context.SaveChanges();

        return new(result.Entity.Id, result.Entity.Name);
    }
}
