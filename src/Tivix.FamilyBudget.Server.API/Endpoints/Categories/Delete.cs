using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.UpdateCategoryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Categories;

public class Delete : EndpointBaseAsync.WithRequest<Guid>.WithActionResult
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("categories/"), Authorize]
    [SwaggerOperation(
    Summary = "Delete category",
    Description = "Delete category",
    OperationId = "Categories_Delete",
    Tags = new[] { "Categories" })
    ]

    public override async Task<ActionResult> HandleAsync([FromQuery] Guid categoryId, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteCategoryCommand(categoryId), cancellationToken);
        return NotFound();
    }
}
