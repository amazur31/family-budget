using MediatR;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.UpdateFinancialEntryCommand;

public record UpdateFinancialEntryCommand(Guid Id, string Name, bool IsExpense, Guid CategoryId) : IRequest<UpdateFinancialEntryResponse>;
public record UpdateFinancialEntryResponse(Guid Id, string Name, bool IsExpense, Guid CategoryId);

internal class UpdateFinancialEntryCommandHandler : IRequestHandler<UpdateFinancialEntryCommand, UpdateFinancialEntryResponse>
{
    ApplicationContext _context;
    public UpdateFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<UpdateFinancialEntryResponse> Handle(UpdateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = _context.FinancialEntries.Single(p=>p.Id == request.Id);
        entry.Name = request.Name;
        entry.IsExpense = request.IsExpense;
        var category = _context.Categories.Single(p=>p.Id == request.CategoryId);
        entry.Category = category;

        await _context.SaveChangesAsync(cancellationToken);

        return new(entry.Id, entry.Name, entry.IsExpense, category.Id);
    }
}
