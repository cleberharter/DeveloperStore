using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IOrderRepository using Entity Framework Core
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of OrderRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public OrderRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new order in the database
    /// </summary>
    /// <param name="user">The order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order</returns>
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// Retrieves all orders
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Return all orders</returns>
    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _context.Orders.Where(x => x.Status == OrderStatus.Active).ToListAsync();
    }
}
