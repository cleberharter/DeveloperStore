using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IOrderRepository
{
    /// <summary>
    /// Creates a new order in the repository
    /// </summary>
    /// <param name="order">The order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order</returns>
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetOrders();
}
