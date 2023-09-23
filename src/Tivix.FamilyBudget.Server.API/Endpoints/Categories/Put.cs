using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.UpdateCategoryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Categories;

public class Put : EndpointBaseAsync.WithRequest<UpdateCategoryCommand>.WithActionResult<UpdateCategoryResponse>
{
    private readonly IMediator _mediator;

    public Put(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("categories/"), Authorize]
    [SwaggerOperation(
    Summary = "Updates category",
    Description = "Updates category",
    OperationId = "Categories_Put",
    Tags = new[] { "Categories" })
    ]

    public override async Task<ActionResult<UpdateCategoryResponse>> HandleAsync([FromBody] UpdateCategoryCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
