using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.Property(b => b.Quantity).IsRequired(true);
        builder.OwnsOne(
            o => o.TotalAmount,
            sa =>
            {
                sa.Property(p => p.Amount).HasColumnName(nameof(OrderItem.TotalAmount)).IsRequired(true);
                sa.Property(p => p.Currency).HasColumnName(nameof(OrderItem.TotalAmount) + nameof(Money.Currency)).HasMaxLength(500).IsRequired(true);

            });

        builder.Navigation(o => o.TotalAmount)
              .IsRequired(true);
    }
}
