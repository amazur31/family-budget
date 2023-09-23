using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgets;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetSharedBudgets;
public record GetSharedBudgetsQuery() : IRequest<GetSharedBudgetsQueryResponse>;
public record GetSharedBudgetsQueryResponseBudget(Guid Id, string Name);
public record GetSharedBudgetsQueryResponse(ICollection<GetSharedBudgetsQueryResponseBudget> Budgets);
public class GetSharedBudgetsQueryHandler : IRequestHandler<GetSharedBudgetsQuery, GetSharedBudgetsQueryResponse>
{
    private readonly ApplicationContext _context;
    private readonly IUserProvider _userProvider;

    public GetSharedBudgetsQueryHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }
    public async Task<GetSharedBudgetsQueryResponse> Handle(GetSharedBudgetsQuery request, CancellationToken cancellationToken)
    {
        var user = _userProvider.UserEntity;
        if(user!.BudgetsAccessible is null)
        {
            return new(new List<GetSharedBudgetsQueryResponseBudget>());
        }

        return new(await _context.Budgets.Where(p => user!.BudgetsAccessible.Contains(p.Id))
            .Select(p => new GetSharedBudgetsQueryResponseBudget(p.Id, p.Name))
            .ToListAsync(cancellationToken: cancellationToken));
    }
}

internal class GetSharedBudgetsQueryHandlerValidator : AbstractValidator<GetSharedBudgetsQuery>
{
    public GetSharedBudgetsQueryHandlerValidator()
    {
        //TODO: Add Validation
    }
}
