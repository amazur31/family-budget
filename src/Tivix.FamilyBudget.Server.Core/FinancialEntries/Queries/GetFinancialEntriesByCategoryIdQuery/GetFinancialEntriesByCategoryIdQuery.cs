using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.UpdateFinancialEntryCommand;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;

public record GetFinancialEntriesByCategoryIdQuery(Guid CategoryId) : IRequest<ICollection<GetFinancialEntriesByCategoryIdResponse>>;
public record GetFinancialEntriesByCategoryIdResponse(Guid Id, string Name, bool IsExpense, Guid CategoryId);

internal class GetFinancialEntriesByCategoryIdQueryHandler : IRequestHandler<GetFinancialEntriesByCategoryIdQuery, ICollection<GetFinancialEntriesByCategoryIdResponse>>
{
    private readonly ApplicationContext _context;
    public GetFinancialEntriesByCategoryIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<ICollection<GetFinancialEntriesByCategoryIdResponse>> Handle(GetFinancialEntriesByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.SingleAsync(p => p.Id == request.CategoryId, cancellationToken: cancellationToken);

        var entries = _context.FinancialEntries.Where(p=>p.Category == category).ToListAsync(cancellationToken: cancellationToken);

        return (await entries).Select(p =>
        {
            return new GetFinancialEntriesByCategoryIdResponse(p.Id, p.Name, p.IsExpense, category.Id);
        }).ToList();
    }
}

internal class GetFinancialEntriesByCategoryIdQueryHandlerValidator : AbstractValidator<GetFinancialEntriesByCategoryIdQuery>
{
    public GetFinancialEntriesByCategoryIdQueryHandlerValidator()
    {
        //TODO: Add Validation
    }
}

