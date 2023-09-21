using MediatR;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands;

public record CreateBudgetCommand(string Name, Guid OwnerId) : IRequest<Guid>;
internal class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Guid>
{
    public Task<Guid> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
