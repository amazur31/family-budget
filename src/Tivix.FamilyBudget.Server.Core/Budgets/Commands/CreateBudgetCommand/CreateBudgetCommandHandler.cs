using MediatR;
using Tivix.FamilyBudget.Server.Core.Budgets.Models;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

public record CreateBudgetCommand(string Name, Guid OwnerId) : IRequest<Budget>;
internal class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Budget>
{
    ApplicationContext _context;
    public CreateBudgetCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Budget> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = new Budget(request.Name, request.OwnerId);

        var result = await _context.Budgets.AddAsync(budget.ToBudgetEntity(), cancellationToken);
        _context.SaveChanges();

        return new(result.Entity);
    }
}
