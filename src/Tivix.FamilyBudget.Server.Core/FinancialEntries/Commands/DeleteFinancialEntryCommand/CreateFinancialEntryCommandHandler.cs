using MediatR;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.DeleteFinancialEntryCommand;

public record DeleteFinancialEntryCommand(Guid Id) : IRequest;

internal class DeleteFinancialEntryCommandHandler : IRequestHandler<DeleteFinancialEntryCommand>
{
    ApplicationContext _context;
    public DeleteFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = _context.FinancialEntries.Single(p=>p.Id == request.Id);
        var budget = _context.FinancialEntries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
