using FluentValidation;
using MediatR;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

public record CreateBudgetCommand(string Name) : IRequest<CreateBudgetResponse>;

public record CreateBudgetResponse(Guid Id, string Name);

internal class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, CreateBudgetResponse>
{
    readonly ApplicationContext _context;
    readonly IUserProvider _userProvider;
    public CreateBudgetCommandHandler(ApplicationContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task<CreateBudgetResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var user = _userProvider.UserEntity;

        var budget = new BudgetEntity() { Id = Guid.NewGuid(), Name = request.Name, User = user! };

        var result = await _context.Budgets.AddAsync(budget, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(result.Entity.Id, result.Entity.Name);
    }
}

internal class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        //TODO: Add User Validation
        //TODO: Add Exists validation

        RuleFor(p => p.Name).NotEmpty();
    }
}
