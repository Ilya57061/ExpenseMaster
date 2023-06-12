using ExpenseMaster.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseMaster.Model.Configuration
{
    public class FinancialGoalConfiguration : IEntityTypeConfiguration<FinancialGoal>
    {
        public void Configure(EntityTypeBuilder<FinancialGoal> builder)
        {
            builder.ToTable("FinancialGoals")
                .HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Property(x=> x.GoalName)
                .IsRequired();

            builder.Property(x=> x.TargetAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.CurrentAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
