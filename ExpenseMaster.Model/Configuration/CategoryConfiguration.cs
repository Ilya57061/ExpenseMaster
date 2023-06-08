using ExpenseMaster.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseMaster.Model.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories")
                .HasKey(x=> x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
