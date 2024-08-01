using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexer.Business.Models;

namespace Nexer.Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.Email)
               .IsRequired()
               .HasColumnType("varchar(300)");

            // 1 : N =< Customer : Products
            builder.HasMany(p => p.Products)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            builder.ToTable("Customers");
        }
    }
}
