﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Queries.GetFinancialEntriesByCategoryIdQuery;

namespace Tivix.FamilyBudget.Server.API.Endpoints.FinancialEntries;

public class Get : EndpointBaseAsync.WithRequest<GetFinancialEntriesByCategoryIdQuery>.WithActionResult<GetFinancialEntriesByCategoryIdResponse>
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

    public override async Task<ActionResult<GetFinancialEntriesByCategoryIdResponse>> HandleAsync([FromQuery] GetFinancialEntriesByCategoryIdQuery request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
