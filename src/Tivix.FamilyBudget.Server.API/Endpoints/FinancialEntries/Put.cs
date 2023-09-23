using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.UpdateFinancialEntryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.FinancialEntries;

public class Put : EndpointBaseAsync.WithRequest<UpdateFinancialEntryCommand>.WithActionResult<UpdateFinancialEntryResponse>
{
    private readonly IMediator _mediator;

    public Put(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("financial-entries/"), Authorize]
    [SwaggerOperation(
    Summary = "Updates financial entry",
    Description = "Updates financial entry",
    OperationId = "FinancialEntries_Put",
    Tags = new[] { "FinancialEntries" })
    ]

    public override async Task<ActionResult<UpdateFinancialEntryResponse>> HandleAsync([FromBody] UpdateFinancialEntryCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
