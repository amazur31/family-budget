using System.Text.Json;
using Tivix.FamilyBudget.Server.Core.Users.Providers;

namespace Tivix.FamilyBudget.Server.API.Middlewares;

internal sealed class UserProviderMiddleware : IMiddleware
{
    private readonly IUserProvider _userProvider;
    public UserProviderMiddleware(IUserProvider userProvider) => _userProvider = userProvider;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if(context.User != null
            && context.User.Identity != null
            && context.User.Identity.IsAuthenticated
            && context.User.Identity.Name != null)
        {
            var id = context.User.Claims.Single(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            await _userProvider.SetUserEntity(id);
        }

        await next(context);
    }
}
