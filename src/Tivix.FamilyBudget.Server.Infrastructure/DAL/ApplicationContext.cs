using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL;
public class ApplicationContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "example@example.com",
                NormalizedEmail = "EXAMPLE@EXAMPLE.COM",
                NormalizedUserName = "EXAMPLE@EXAMPLE.COM",
                Email = "example@example.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEJ1yKBk/2UNYl6o16aimTV6gUrwQlKHGRcLbO14Oam8e31VG7YxHgEOdWau4AoaXNw==",
                SecurityStamp = "HE2MFWR5BLKWMUN3KVXTVSMILHRWTQYD",
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
