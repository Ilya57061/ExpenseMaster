using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseMaster.DAL.Configuration
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder) 
        {
            builder.ToTable("Incomes")
                .HasKey(x => x.Id);

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
                .HasForeignKey(x=> x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnType("date");
        }
    }
}
