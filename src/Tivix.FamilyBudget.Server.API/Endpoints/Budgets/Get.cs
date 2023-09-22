﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetById;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class Get : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<GetBudgetByIdResponse>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/budgets/{id}")]
    [SwaggerOperation(
    Summary = "Gets a Budget",
    Description = "Gets a Budget",
    OperationId = "Budget_Get",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult<GetBudgetByIdResponse>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetBudgetByIdQuery(id), cancellationToken));
    }
}
