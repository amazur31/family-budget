using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.UpdateCategoryCommand;
public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<UpdateCategoryResponse>;
public record UpdateCategoryResponse(Guid Id, string Name);

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    readonly ApplicationContext _context;
    public UpdateCategoryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await  _context.Categories.FirstAsync(p =>p.Id == request.Id, cancellationToken: cancellationToken);
        category.Name = request.Name;
        await _context.SaveChangesAsync();
        return new(category.Id, category.Name);
    }
}

internal class UpdateCategoryCommandHandlerValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandHandlerValidator(IUserProvider provider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Id).SetValidator(new UserCategoryValidator(provider, applicationContext));
    }
}
