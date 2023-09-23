using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.API.Endpoints.FinancialEntries;

public class Get : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<GetCategoriesByBudgetIdResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("financial-entries/"), Authorize]
    [SwaggerOperation(
    Summary = "Gets financial entries for category",
    Description = "Gets financial entries for category",
    OperationId = "FinancialEntries_Get",
    Tags = new[] { "FinancialEntries" })
    ]

    public override async Task<ActionResult<GetCategoriesByBudgetIdResponse>> HandleAsync([FromQuery] Guid categoryId, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetCategoriesByBudgetIdQuery(categoryId), cancellationToken));
    }
}
