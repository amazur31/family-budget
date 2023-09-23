using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.DeleteBudgetCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class Delete : EndpointBaseAsync.WithRequest<DeleteBudgetCommand>.WithActionResult
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("budgets/"), Authorize]
    [SwaggerOperation(
    Summary = "Delete budget",
    Description = "Delete budget",
    OperationId = "Budgets_Delete",
    Tags = new[] { "Budgets" })
    ]

    public override async Task<ActionResult> HandleAsync([FromBody] DeleteBudgetCommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return NotFound();
    }
}
