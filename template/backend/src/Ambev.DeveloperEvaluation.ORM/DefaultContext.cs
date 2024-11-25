using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(p =>
        {
            p.HasData(
                    new User { Id = new Guid("18df26ad-e70e-4162-8be4-d346e47afc7a"), Username = "Cliente Teste", Email = "teste@gmail.com", Phone = "51984683256", Status = UserStatus.Active, 
                        Role = UserRole.Customer, CreatedAt = DateTime.UtcNow }
                
                );
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.HasData(
                new Product { Id = new Guid("4019ac92-8b4e-45af-a5ec-dbb07e09377b"), Name = "Monitor" },
                new Product { Id = new Guid("f6e6ae42-2f32-463b-8386-a276ae7ba182"), Name = "Teclado" },
                new Product { Id = new Guid("6201dcdd-17eb-467a-a742-633ee84d93e7"), Name = "Mouse" },
                new Product { Id = new Guid("2294dfa9-8fe5-433b-8d07-3b1ff18e4a56"), Name = "Notebook" }
            );
            p.OwnsOne(pr => pr.Price)
                .HasData(
                    new { ProductId = new Guid("4019ac92-8b4e-45af-a5ec-dbb07e09377b"), Currency = Currencies.Real.ToString(), Amount = 756m },
                    new { ProductId = new Guid("f6e6ae42-2f32-463b-8386-a276ae7ba182"), Currency = Currencies.Real.ToString(), Amount = 95.6m },
                    new { ProductId = new Guid("6201dcdd-17eb-467a-a742-633ee84d93e7"), Currency = Currencies.Real.ToString(), Amount = 55.5m },
                    new { ProductId = new Guid("2294dfa9-8fe5-433b-8d07-3b1ff18e4a56"), Currency = Currencies.Real.ToString(), Amount = 5850m }
                );
        });
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}