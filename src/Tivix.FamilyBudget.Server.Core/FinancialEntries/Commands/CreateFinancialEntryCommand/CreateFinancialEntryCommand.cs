using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.FinancialEntries.Commands.CreateFinancialEntryCommand;

public record CreateFinancialEntryCommand(string Name, bool IsExpense, Guid CategoryId) : IRequest<CreateFinancialEntryResponse>;
public record CreateFinancialEntryResponse(Guid Id, string Name, bool IsExpense, Guid CategoryId);

internal class CreateFinancialEntryCommandHandler : IRequestHandler<CreateFinancialEntryCommand, CreateFinancialEntryResponse>
{
    readonly ApplicationContext _context;
    public CreateFinancialEntryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CreateFinancialEntryResponse> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstAsync(p =>p.Id == request.CategoryId, cancellationToken: cancellationToken);
        var budget = await _context.FinancialEntries.AddAsync(new FinancialEntryEntity()
        {
            Id = new Guid(),
            Category = category,
            IsExpense = request.IsExpense,
            Name = request.Name
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(budget.Entity.Id, budget.Entity.Name, budget.Entity.IsExpense, category.Id);
    }
}

internal class CreateFinancialEntryCommandHandlerValidator : AbstractValidator<CreateFinancialEntryCommand>
{
    public CreateFinancialEntryCommandHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p=>p.Name).NotEmpty();
        RuleFor(p => p.CategoryId).SetValidator(new UserCategoryValidator(userProvider, applicationContext));
    }
}
