using FluentValidation;
using MediatR;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

public record ShareBudgetCommand(Guid BudgetId, Guid UserId) : IRequest;

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
       
    }
}

internal class ShareBudgetCommandHandlerValidator : AbstractValidator<ShareBudgetCommand>
{
    public ShareBudgetCommandHandlerValidator()
    {
        //TODO: Add Validation
    }
}
