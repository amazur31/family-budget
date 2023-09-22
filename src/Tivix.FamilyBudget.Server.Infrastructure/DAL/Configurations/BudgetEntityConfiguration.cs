using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Configurations;
internal class BudgetEntityConfiguration : IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.User).IsRequired();
    }
}
