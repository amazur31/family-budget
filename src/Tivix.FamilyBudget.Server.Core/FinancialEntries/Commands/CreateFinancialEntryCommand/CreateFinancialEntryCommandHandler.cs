using MediatR;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
public record CreateFinancialEntryCommand(string Name, bool IsExpense, Guid CategoryId) : IRequest<CreateFinancialEntryResponse>;
public record CreateFinancialEntryResponse(Guid Id, string Name, bool IsExpense, Guid CategoryId);

internal class CreateFinancialEntryCommandHandler : IRequestHandler<CreateFinancialEntryCommand, CreateFinancialEntryResponse>
{
    ApplicationContext _context;
    public CreateFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CreateFinancialEntryResponse> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var category = _context.Categories.First(p=>p.Id == request.CategoryId);
        var budget = _context.FinancialEntries.Add(new FinancialEntryEntity()
        {
            Id = new Guid(),
            Category = category,
            IsExpense = request.IsExpense,
            Name = request.Name
        });

        await _context.SaveChangesAsync();

        return new(budget.Entity.Id, budget.Entity.Name, budget.Entity.IsExpense, category.Id);
    }
}
