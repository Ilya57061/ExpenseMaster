using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseMaster.DAL.Configuration
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("Budgets")
                .HasKey(x=> x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x=> x.CategoryId)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Limit)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x=> x.WarningThreshold)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
