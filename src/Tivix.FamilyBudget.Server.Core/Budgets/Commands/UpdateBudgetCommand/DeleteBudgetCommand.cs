using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.UpdateBudgetCommand;

public record UpdateBudgetCommand(Guid Id, string Name) : IRequest<UpdateBudgetResponse>;
public record UpdateBudgetResponse(Guid Id, string Name);

internal class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, UpdateBudgetResponse>
{
    readonly ApplicationContext _context;
    readonly IUserProvider _userProvider;
    public UpdateBudgetCommandHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task<UpdateBudgetResponse> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.Budgets.FirstAsync(p=>p.Id==request.Id);

        budget.Name = request.Name;

        _context.Budgets.Update(budget);

        await _context.SaveChangesAsync(cancellationToken);

        return new(budget.Id, budget.Name);
    }
}

internal class UpdateBudgetCommandHandlerValidator : AbstractValidator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.Id).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
    }
}
