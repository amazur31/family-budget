using MediatR;
using Tivix.FamilyBudget.Server.Core.Budgets.Models;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetById;
public record GetBudgetByIdQuery(Guid Id) : IRequest<Budget>;
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, Budget>
{
    public async Task<Budget> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        return new Budget();
    }
}
