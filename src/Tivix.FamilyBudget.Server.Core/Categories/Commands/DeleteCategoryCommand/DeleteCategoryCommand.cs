using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.UpdateCategoryCommand;
public record DeleteCategoryCommand(Guid Id) : IRequest;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    readonly ApplicationContext _context;
    public DeleteCategoryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);
        _context.Categories.Remove(category);
        _ = _context.SaveChangesAsync(cancellationToken);
    }
}

internal class DeleteCategoryCommandHandlerValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandHandlerValidator(IUserProvider provider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.Id).SetValidator(new UserCategoryValidator(provider, applicationContext));
    }
}
