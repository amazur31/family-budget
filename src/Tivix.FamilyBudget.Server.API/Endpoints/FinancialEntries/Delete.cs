using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.DeleteFinancialEntryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.FinancialEntries;

public class Delete : EndpointBaseAsync.WithRequest<Guid>.WithActionResult
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("financial-entries/"), Authorize]
    [SwaggerOperation(
    Summary = "Delete financial entry",
    Description = "Delete financial entry",
    OperationId = "FinancialEntries_Delete",
    Tags = new[] { "FinancialEntries" })
    ]

    public override async Task<ActionResult> HandleAsync([FromQuery] Guid financialEntryId, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteFinancialEntryCommand(financialEntryId), cancellationToken);
        return NotFound();
    }
}
