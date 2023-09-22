using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Users.Providers;
public interface IUserProvider
{
    UserEntity? UserEntity { get; }

    Task SetUserEntity(string userId);
}
