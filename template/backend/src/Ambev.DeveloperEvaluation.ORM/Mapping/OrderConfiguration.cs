using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Branch).IsRequired().HasMaxLength(100);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(8);

        builder.OwnsOne(
            o => o.TotalAmount,
            sa =>
            {
                sa.Property(p => p.Amount).HasColumnName(nameof(Order.TotalAmount)).IsRequired(true);
                sa.Property(p => p.Currency).HasColumnName(nameof(Order.TotalAmount) + nameof(Money.Currency)).HasMaxLength(8).IsRequired(true);

            });

        builder.Navigation(o => o.TotalAmount)
              .IsRequired(true);

        builder.OwnsOne(
            o => o.Discount,
            sa =>
            {
                sa.Property(p => p.Amount).HasColumnName(nameof(Order.Discount)).IsRequired(true);
                sa.Property(p => p.Currency).HasColumnName(nameof(Order.Discount) + nameof(Money.Currency)).HasMaxLength(8).IsRequired(true);
            });

        builder.Navigation(o => o.Discount)
              .IsRequired(true);
    }
}
