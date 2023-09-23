using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.ShareBudgetCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets.Shared;

public class Post : EndpointBaseAsync.WithRequest<ShareBudgetCommand>.WithActionResult
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/budgets/share"), Authorize]
    [SwaggerOperation(
    Summary = "Shares a Budget",
    Description = "Shares a Budget",
    OperationId = "Budgets/share_Post",
    Tags = new[] { "Budgets" })
]
    public override async Task<ActionResult> HandleAsync(ShareBudgetCommand request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
