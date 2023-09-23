using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.DeleteBudgetCommand;

public record DeleteBudgetCommand(Guid Id) : IRequest;
internal class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
{
    readonly ApplicationContext _context;
    readonly IUserProvider _userProvider;
    public DeleteBudgetCommandHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.Budgets.FirstAsync(p =>p.Id==request.Id, cancellationToken: cancellationToken);

        _context.Budgets.Remove(budget);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

internal class DeleteBudgetCommandHandlerValidator : AbstractValidator<DeleteBudgetCommand>
{
    public DeleteBudgetCommandHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.Id).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
    }
}
