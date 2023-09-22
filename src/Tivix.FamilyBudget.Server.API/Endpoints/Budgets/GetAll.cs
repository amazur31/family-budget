using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgets;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class GetAll : EndpointBaseAsync.WithoutRequest.WithActionResult<GetBudgetsQueryResponse>
{
    private readonly IMediator _mediator;

    public GetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/budgets"), Authorize]
    [SwaggerOperation(
    Summary = "Gets budgets for logged in user",
    Description = "Gets budgets for logged in user",
    OperationId = "Budgets_Get",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult<GetBudgetsQueryResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetBudgetsQuery(), cancellationToken));
    }
}
