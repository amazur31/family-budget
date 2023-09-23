using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetSharedBudgets;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets.Shared;

public class Get : EndpointBaseAsync.WithoutRequest.WithActionResult<GetSharedBudgetsQueryResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("budgets/shared"), Authorize]
    [SwaggerOperation(
    Summary = "Gets budgets you have access to",
    Description = "Gets budgets you have access to",
    OperationId = "Budgets/shared_Get",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult<GetSharedBudgetsQueryResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetSharedBudgetsQuery(), cancellationToken));
    }
}
