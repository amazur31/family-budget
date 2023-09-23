using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.DeleteFinancialEntryCommand;

public record DeleteFinancialEntryCommand(Guid Id) : IRequest;

internal class DeleteFinancialEntryCommandHandler : IRequestHandler<DeleteFinancialEntryCommand>
{
    readonly ApplicationContext _context;
    public DeleteFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _context.FinancialEntries.SingleAsync(p =>p.Id == request.Id, cancellationToken: cancellationToken);
        _context.FinancialEntries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
