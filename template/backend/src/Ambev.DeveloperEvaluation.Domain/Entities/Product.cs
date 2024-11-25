using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public Money Price { get; set; }

    public Product()
    {
        
    }

    public Product(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Product(Guid id, string name, Money price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
