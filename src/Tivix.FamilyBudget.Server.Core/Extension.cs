using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tivix.FamilyBudget.Server.Core.Common.Validation;
using Tivix.FamilyBudget.Server.Core.Users.Providers;

namespace Tivix.FamilyBudget.Server.Core;

public static class Extension
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Extension).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddValidatorsFromAssembly(typeof(Extension).Assembly);
    }
}
