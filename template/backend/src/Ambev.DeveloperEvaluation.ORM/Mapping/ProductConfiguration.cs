using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.Name).HasMaxLength(500).IsRequired(true);
        builder.OwnsOne(
            o => o.Price,
            sa =>
            {
                sa.Property(p => p.Amount).HasColumnName(nameof(Product.Price)).IsRequired(true);
                sa.Property(p => p.Currency).HasColumnName(nameof(Money.Currency)).HasMaxLength(8).IsRequired(true);
            });

        builder.Navigation(o => o.Price)
              .IsRequired(true);
    }
}

