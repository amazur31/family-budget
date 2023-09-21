using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Categories.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

public record GetCategoriesByBudgetIdQuery(Guid BudgetId) : IRequest<ICollection<Category>>;
internal class GetCategoriesByBudgetIdQueryHandler : IRequestHandler<GetCategoriesByBudgetIdQuery, ICollection<Category>>
{
    private readonly ApplicationContext _context;
    public GetCategoriesByBudgetIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Category>> Handle(GetCategoriesByBudgetIdQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Categories.Where(p => p.BudgetId == request.BudgetId);
        return await result.Select(p=>new Category(p)).ToListAsync(cancellationToken: cancellationToken);
    }
}
