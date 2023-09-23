using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.ShareBudgetCommand;

public record ShareBudgetCommand(Guid BudgetId, Guid TargetUserId) : IRequest;

internal class ShareBudgetCommandHandler : IRequestHandler<ShareBudgetCommand>
{
    readonly ApplicationContext _context;
    public ShareBudgetCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(ShareBudgetCommand request, CancellationToken cancellationToken)
    {
        var targetUser = await _context.Users.SingleAsync(p => p.Id == request.TargetUserId, cancellationToken: cancellationToken);
        targetUser.BudgetsAccessible ??= new List<Guid>();
        targetUser.BudgetsAccessible.Add(request.BudgetId);
        _context.SaveChanges();
    }
}

internal class ShareBudgetCommandHandlerValidator : AbstractValidator<ShareBudgetCommand>
{
    public ShareBudgetCommandHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.BudgetId).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
        RuleFor(p => p.TargetUserId).SetValidator(new UserExistsValidator(applicationContext));
    }
}
