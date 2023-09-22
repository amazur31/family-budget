using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

namespace Tivix.FamilyBudget.Server.Infrastructure.DAL.Configurations;
internal class FinancialEntryEntityConfiguration : IEntityTypeConfiguration<FinancialEntryEntity>
{
    public void Configure(EntityTypeBuilder<FinancialEntryEntity> builder)
    {
        builder.ToTable("FinancialEntries");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.Category).IsRequired();

        builder.Property(p => p.Name).IsRequired();

        builder.Property(p => p.IsExpense).IsRequired();
    }
}
