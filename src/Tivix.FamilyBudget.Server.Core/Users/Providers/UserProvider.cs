﻿using Microsoft.EntityFrameworkCore;
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
        UserEntity = await _context.Users.Include(p=>p.Budgets).FirstOrDefaultAsync(x => x.Id == userId);
    }
}
