using ExpenseMaster.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseMaster.Model.Configuration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder) 
        {
            builder.ToTable("Expenses")
                .HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Property(x=> x.CategoryId)
                .IsRequired();

            builder.HasOne(x=> x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);

            builder.Property(x=> x.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnType("date")
                .IsRequired();
        }
    }
}
