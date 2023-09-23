using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.UnshareBudgetCommandHandler;

public record UnshareBudgetCommand(Guid BudgetId, Guid TargetUserId) : IRequest;

internal class UnshareBudgetCommandHandler : IRequestHandler<UnshareBudgetCommand>
{
    readonly ApplicationContext _context;
    public UnshareBudgetCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(UnshareBudgetCommand request, CancellationToken cancellationToken)
    {
        var targetUser = await _context.Users.SingleAsync(p => p.Id == request.TargetUserId, cancellationToken: cancellationToken);
        targetUser.BudgetsAccessible!.Remove(request.BudgetId);
        _context.SaveChanges();
    }
}

internal class UnshareBudgetCommandHandlerValidator : AbstractValidator<UnshareBudgetCommand>
{
    public UnshareBudgetCommandHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.BudgetId).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
        RuleFor(p => p.TargetUserId).SetValidator(new UserExistsValidator(applicationContext));
    }
}
