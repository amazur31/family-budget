using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Budgets.Commands.CreateBudgetCommand;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Categories.Commands.CreateCategoryCommand;
public record CreateCategoryCommand(string Name, Guid BudgetId) : IRequest<CreateCategoryResponse>;
public record CreateCategoryResponse(Guid Id, string Name);

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    readonly ApplicationContext _context;
    public CreateCategoryCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.Budgets.FirstOrDefaultAsync(p => p.Id == request.BudgetId, cancellationToken: cancellationToken);
        var categoryEntity = new CategoryEntity() { Budget = budget!, Id = Guid.NewGuid(), Name = request.Name };
        var result = await _context.Categories.AddAsync(categoryEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new(result.Entity.Id, result.Entity.Name);
    }
}

internal class CreateCategoryCommandHandlerValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandHandlerValidator()
    {
        //TODO: Add Validation
    }
}
