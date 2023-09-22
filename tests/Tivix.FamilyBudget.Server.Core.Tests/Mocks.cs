using NSubstitute;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Core.Tests;
public static class Mocks
{
    public static UserEntity User = new() { Id = Guid.NewGuid()  };
    public static IUserProvider UserProviderMock = Substitute.For<IUserProvider>();
}
