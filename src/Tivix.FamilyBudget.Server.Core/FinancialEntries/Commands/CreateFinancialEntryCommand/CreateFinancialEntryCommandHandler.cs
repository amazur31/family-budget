using MediatR;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
public record CreateFinancialEntryCommand(string Name, bool IsExpense) : IRequest<CreateFinancialEntryResponse>;
public record CreateFinancialEntryResponse(Guid Id, string Name, bool IsExpense);

internal class CreateFinancialEntryCommandHandler : IRequestHandler<CreateFinancialEntryCommand, CreateFinancialEntryResponse>
{
    ApplicationContext _context;
    public CreateFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CreateFinancialEntryResponse> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var budget = _context.Budgets.FirstOrDefault(p => p.Id == request.BudgetId);
        var categoryEntity = new CategoryEntity() { Budget = budget!, Id = Guid.NewGuid(), Name = request.Name };
        var result = await _context.Categories.AddAsync(categoryEntity);
        _context.SaveChanges();

        return new(result.Entity.Id, result.Entity.Name);
    }
}
