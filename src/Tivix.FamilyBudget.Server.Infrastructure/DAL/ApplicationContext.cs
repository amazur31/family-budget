using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL;
public class ApplicationContext : IdentityDbContext<UserEntity>
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
}
