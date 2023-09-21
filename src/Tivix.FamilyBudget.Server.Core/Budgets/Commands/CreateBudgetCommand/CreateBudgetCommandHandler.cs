using MediatR;
using Tivix.FamilyBudget.Server.Core.Budgets.Models;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

public record CreateBudgetCommand(string Name, Guid OwnerId) : IRequest<Budget>;
internal class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Budget>
{
    public async Task<Budget> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        return new Budget();
    }
}
