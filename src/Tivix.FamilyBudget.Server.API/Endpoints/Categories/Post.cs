using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Categories;

public class Post : EndpointBaseAsync.WithRequest<CreateCategoryCommand>.WithActionResult<CreateCategoryResponse>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/categories")]
    [SwaggerOperation(
    Summary = "Creates category",
    Description = "Creates category",
    OperationId = "Category_Create",
    Tags = new[] { "Categories" })
    ]

    public override async Task<ActionResult<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
