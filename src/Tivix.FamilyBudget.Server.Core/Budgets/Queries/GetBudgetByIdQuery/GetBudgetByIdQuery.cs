using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Core.Users.Validators;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Budgets.Queries.GetBudgetByIdQuery;
public record GetBudgetByIdQuery(Guid Id) : IRequest<GetBudgetByIdResponse>;

public record GetBudgetByIdResponse(Guid Id, string Name);
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, GetBudgetByIdResponse>
{
    private readonly ApplicationContext _context;
    public GetBudgetByIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<GetBudgetByIdResponse> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Budgets.FirstAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        return new(result!.Id, result.Name);
    }
}

internal class GetBudgetByIdQueryHandlerValidator : AbstractValidator<GetBudgetByIdQuery>
{
    public GetBudgetByIdQueryHandlerValidator(IUserProvider userProvider, ApplicationContext applicationContext)
    {
        RuleFor(p => p.Id).SetValidator(new UserBudgetValidator(userProvider, applicationContext));
    }
}
