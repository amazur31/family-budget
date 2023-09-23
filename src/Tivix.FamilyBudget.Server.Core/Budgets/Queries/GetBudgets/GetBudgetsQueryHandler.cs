using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgets;
public record GetBudgetsQuery() : IRequest<GetBudgetsQueryResponse>;

public record GetBudgetsQueryResponseBudget(Guid Id, string Name);
public record GetBudgetsQueryResponse(ICollection<GetBudgetsQueryResponseBudget> Budgets);
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetsQuery, GetBudgetsQueryResponse>
{
    private readonly ApplicationContext _context;
    private readonly IUserProvider _userProvider;

    public GetBudgetByIdQueryHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }
    public async Task<GetBudgetsQueryResponse> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        var user = _userProvider.UserEntity;
        var budgets = await _context.Budgets.AsNoTracking().Where(p => p.User == user).ToListAsync(cancellationToken: cancellationToken);
        return new(budgets.Select(p => new GetBudgetsQueryResponseBudget(p.Id, p.Name)).ToList());
    }
}
