using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Categories.Queries.GetCategoriesByBudgetId;

public record GetCategoriesByBudgetIdQuery(Guid BudgetId) : IRequest<ICollection<GetCategoriesByBudgetIdResponse>>;

public record GetCategoriesByBudgetIdResponse(Guid Id, string Name);
internal class GetCategoriesByBudgetIdQueryHandler : IRequestHandler<GetCategoriesByBudgetIdQuery, ICollection<GetCategoriesByBudgetIdResponse>>
{
    private readonly ApplicationContext _context;
    public GetCategoriesByBudgetIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<ICollection<GetCategoriesByBudgetIdResponse>> Handle(GetCategoriesByBudgetIdQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Categories.Where(p => p.Budget.Id == request.BudgetId);
        return await result.Select(p=>new GetCategoriesByBudgetIdResponse(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
    }
}

internal class GetCategoriesByBudgetIdQueryHandlerValidator : AbstractValidator<GetCategoriesByBudgetIdQuery>
{
    public GetCategoriesByBudgetIdQueryHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.BudgetId).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
    }
}
