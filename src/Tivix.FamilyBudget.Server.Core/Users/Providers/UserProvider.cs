using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Users.Providers;
public class UserProvider : IUserProvider
{
    ApplicationContext _context;
    public UserProvider(ApplicationContext context)
    {
        _context = context;
    }

    public UserEntity? UserEntity { get; private set; }

    public async Task SetUserEntity(string userId)
    {
        var userGuid = Guid.Parse(userId);
        UserEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userGuid);
    }
}
