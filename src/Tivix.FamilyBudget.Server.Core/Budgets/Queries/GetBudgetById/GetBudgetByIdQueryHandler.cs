using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Budgets.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetById;
public record GetBudgetByIdQuery(Guid Id) : IRequest<Budget>;
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, Budget>
{
    private readonly ApplicationContext _context;
    public GetBudgetByIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<Budget> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Budgets.SingleAsync(p => p.Id == request.Id);
        return new(result);
    }
}
