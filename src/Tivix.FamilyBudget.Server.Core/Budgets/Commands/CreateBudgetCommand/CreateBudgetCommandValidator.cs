using FluentValidation;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

internal class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        //TODO: Add User Validation
        //TODO: Add Exists validation

        RuleFor(p => p.OwnerId).NotEmpty();

        RuleFor(p => p.Name).NotEmpty();
    }
}
