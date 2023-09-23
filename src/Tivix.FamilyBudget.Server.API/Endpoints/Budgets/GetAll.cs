using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgets;
using Tivix.FamilyBudget.Server.Core.Common.Pagination;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

//Needed for multiple binds in ApiEndpoints
public record GetCategoriesByBudgetIdRequest
{
    [FromQuery]
    public Pagination? Pagination { get; set; }
}

public class GetAll : EndpointBaseAsync.WithRequest<GetCategoriesByBudgetIdRequest>.WithActionResult<GetBudgetsQueryResponse>
{
    private readonly IMediator _mediator;

    public GetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/budgets")]
    [SwaggerOperation(
    Summary = "Gets own budgets for logged in user",
    Description = "Gets own budgets for logged in user",
    OperationId = "Budgets_Get",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult<GetBudgetsQueryResponse>> HandleAsync([FromQuery] GetCategoriesByBudgetIdRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetBudgetsQuery(request.Pagination), cancellationToken));
    }
}
