using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Categories;

public class Get : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<GetCategoriesByBudgetIdResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories/"), Authorize]
    [SwaggerOperation(
    Summary = "Gets categories for budget",
    Description = "Gets categories for budget",
    OperationId = "Category_Get",
    Tags = new[] { "Categories" })
    ]

    public override async Task<ActionResult<GetCategoriesByBudgetIdResponse>> HandleAsync([FromQuery] Guid budgetId, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetCategoriesByBudgetIdQuery(budgetId), cancellationToken));
    }
}
