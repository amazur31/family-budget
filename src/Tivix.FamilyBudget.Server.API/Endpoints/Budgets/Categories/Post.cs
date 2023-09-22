using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;

namespace Tivix.FamilyBudget.Server.API.Endpoints.Budgets.Categories;

//Needed for multiple binds in ApiEndpoints
public record CreateCategoryPostRequest
{
    [FromRoute]
    public Guid budgetId { get; set; }
    [FromBody]
    public NamePost Name { get; set; }

    public record NamePost(string Name);
}

public class Post : EndpointBaseAsync.WithRequest<CreateCategoryPostRequest>.WithActionResult<CreateCategoryResponse>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("budgets/{budgetId}/categories/"), Authorize]
    [SwaggerOperation(
    Summary = "Creates category",
    Description = "Creates category",
    OperationId = "Category_Post",
    Tags = new[] { "Budgets" })
]
    public override async Task<ActionResult<CreateCategoryResponse>> HandleAsync([FromRoute][FromBody] CreateCategoryPostRequest request, CancellationToken cancellationToken = default)
    {
        CreateCategoryCommand command = new(request!.Name.Name, request.budgetId);
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
