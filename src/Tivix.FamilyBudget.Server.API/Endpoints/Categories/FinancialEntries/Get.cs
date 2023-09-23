using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Categories.FinancialEntries;

public class Get : EndpointBaseAsync.WithRequest<GetFinancialEntriesByCategoryIdQuery>.WithActionResult<GetFinancialEntriesByCategoryIdResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories/{CategoryId}/financial-entries")]
    [SwaggerOperation(
    Summary = "Gets financial entries for category",
    Description = "Gets financial entries for category",
    OperationId = "Categories/financial-entries_Get",
    Tags = new[] { "Categories" })
    ]

    public override async Task<ActionResult<GetFinancialEntriesByCategoryIdResponse>> HandleAsync([FromRoute] GetFinancialEntriesByCategoryIdQuery request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
