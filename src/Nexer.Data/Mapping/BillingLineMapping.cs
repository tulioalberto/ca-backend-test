using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexer.Business.Models;

public class BillingLineMapping : IEntityTypeConfiguration<BillingLine>
{
    public void Configure(EntityTypeBuilder<BillingLine> builder)
    {
        builder.HasKey(bl => bl.Id);

        builder.Property(bl => bl.Description)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(bl => bl.Quantity)
            .IsRequired();

        builder.Property(bl => bl.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bl => bl.Subtotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(bl => bl.Product)
            .WithMany()
            .HasForeignKey(bl => bl.ProductId);

        builder.HasOne(bl => bl.Billing)
           .WithMany(b => b.BillingLines)
           .HasForeignKey(bl => bl.BillingLineId);

        builder.ToTable("BillingLines");
    }
}