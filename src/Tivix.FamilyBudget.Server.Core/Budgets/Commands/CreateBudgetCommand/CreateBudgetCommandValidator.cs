﻿using FluentValidation;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;

internal class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(p => p.OwnerId).NotEmpty();

        RuleFor(p => p.Name).NotEmpty();
    }
}