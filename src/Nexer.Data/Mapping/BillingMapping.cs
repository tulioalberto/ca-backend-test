using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexer.Business.Models;

public class BillingMapping : IEntityTypeConfiguration<Billing>
{
    public void Configure(EntityTypeBuilder<Billing> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.InvoiceNumber)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(b => b.Date)
            .IsRequired();

        builder.Property(b => b.DueDate)
            .IsRequired();

        builder.Property(b => b.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.Currency)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.HasOne(b => b.Customer)
            .WithMany(c => c.Billings)
            .HasForeignKey(b => b.CustomerId);

        builder.HasMany(b => b.BillingLines)
            .WithOne(bl => bl.Billing)
            .HasForeignKey(bl => bl.BillingLineId);

        builder.ToTable("Billings");
    }
}