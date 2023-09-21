using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Core.Budgets.Models;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class Post : EndpointBaseAsync.WithRequest<CreateBudgetCommand>.WithActionResult<Budget>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/budgets")]
    [SwaggerOperation(
    Summary = "Creates a new Budget",
    Description = "Creates a new Budget",
    OperationId = "Budget_Create",
    Tags = new[] { "Budgets" })
]
    public override async Task<ActionResult<Budget>> HandleAsync(CreateBudgetCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
