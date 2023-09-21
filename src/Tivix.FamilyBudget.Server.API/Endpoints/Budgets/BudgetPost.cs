using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class BudgetPost : EndpointBaseAsync.WithRequest<CreateBudgetCommand>.WithActionResult<Guid>
{
    public override Task<ActionResult<Guid>> HandleAsync(CreateBudgetCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
