using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.ShareBudgetCommandHandler;

public record ShareBudgetCommand(Guid BudgetId, Guid TargetUserId) : IRequest;

internal class ShareBudgetCommandHandler : IRequestHandler<ShareBudgetCommand>
{
    readonly ApplicationContext _context;
    readonly IUserProvider _userProvider;
    public ShareBudgetCommandHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task Handle(ShareBudgetCommand request, CancellationToken cancellationToken)
    {
        var targetUser = await _context.Users.SingleAsync(p => p.Id == request.TargetUserId);
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
        RuleFor(p => p.TargetUserId).SetValidator(new UserExistsValidator(userProvider, applicationContext));
    }
}
