using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets;

public class Post : EndpointBaseAsync.WithRequest<CreateBudgetCommand>.WithActionResult<CreateBudgetResponse>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/budgets"), Authorize]
    [SwaggerOperation(
    Summary = "Creates a new Budget",
    Description = "Creates a new Budget",
    OperationId = "Budget_Post",
    Tags = new[] { "Budgets" })
]
    public override async Task<ActionResult<CreateBudgetResponse>> HandleAsync(CreateBudgetCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
