using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Common.Pagination;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgets;
public record GetBudgetsQuery(Pagination? Pagination) : IRequest<GetBudgetsQueryResponse>;

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
        var budgetsQuery = _context.Budgets.AsNoTracking().OrderBy(p => p.Id).Select(p => p);
        if (request.Pagination != null) 
        {
            if(request.Pagination.Keyset != null)
            {
                budgetsQuery = budgetsQuery.Where(b => b.Id > request.Pagination.Keyset);
            }
            if(request.Pagination.Amount != null) 
            {
                budgetsQuery = budgetsQuery.Take(request.Pagination.Amount.Value);
            }
        }
        var budgets = await budgetsQuery.ToListAsync(cancellationToken: cancellationToken);
        return new(budgets.Select(p => new GetBudgetsQueryResponseBudget(p.Id, p.Name)).ToList());
    }
}
