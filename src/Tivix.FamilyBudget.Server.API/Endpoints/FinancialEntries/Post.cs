using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.CreateFinancialEntryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.FinancialEntries;

public class Post : EndpointBaseAsync.WithRequest<CreateFinancialEntryCommand>.WithActionResult<CreateFinancialEntryResponse>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("financial-entries/"), Authorize]
    [SwaggerOperation(
    Summary = "Creates financial entry",
    Description = "Creates financial entry",
    OperationId = "FinancialEntries_Post",
    Tags = new[] { "FinancialEntries" })
    ]

    public override async Task<ActionResult<CreateFinancialEntryResponse>> HandleAsync([FromBody] CreateFinancialEntryCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
