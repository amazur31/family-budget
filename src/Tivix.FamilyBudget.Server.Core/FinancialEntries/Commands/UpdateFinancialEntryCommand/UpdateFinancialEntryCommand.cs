using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.UpdateFinancialEntryCommand;

public record UpdateFinancialEntryCommand(Guid Id, string Name, bool IsExpense, Guid CategoryId) : IRequest<UpdateFinancialEntryResponse>;
public record UpdateFinancialEntryResponse(Guid Id, string Name, bool IsExpense, Guid CategoryId);

internal class UpdateFinancialEntryCommandHandler : IRequestHandler<UpdateFinancialEntryCommand, UpdateFinancialEntryResponse>
{
    readonly ApplicationContext _context;
    public UpdateFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<UpdateFinancialEntryResponse> Handle(UpdateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _context.FinancialEntries.SingleAsync(p =>p.Id == request.Id, cancellationToken: cancellationToken);
        entry.Name = request.Name;
        entry.IsExpense = request.IsExpense;
        var category = await _context.Categories.SingleAsync(p =>p.Id == request.CategoryId, cancellationToken: cancellationToken);
        entry.Category = category;

        await _context.SaveChangesAsync(cancellationToken);

        return new(entry.Id, entry.Name, entry.IsExpense, category.Id);
    }
}

internal class UpdateFinancialEntryCommandHandlerValidator : AbstractValidator<UpdateFinancialEntryCommand>
{
    public UpdateFinancialEntryCommandHandlerValidator()
    {
        //TODO: Add Validation
    }
}
