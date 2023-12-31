﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets.Categories;

public class Get : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<GetCategoriesByBudgetIdResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("budgets/{budgetId}/categories"), Authorize]
    [SwaggerOperation(
    Summary = "Gets categories for budget",
    Description = "Gets categories for budget",
    OperationId = "Budgets/categories_Get",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult<GetCategoriesByBudgetIdResponse>> HandleAsync([FromRoute] Guid budgetId, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetCategoriesByBudgetIdQuery(budgetId), cancellationToken));
    }
}
