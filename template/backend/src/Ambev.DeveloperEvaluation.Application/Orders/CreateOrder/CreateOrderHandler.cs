using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var id = Guid.NewGuid();

        //Create Order
        var order = new Order(id, command.UserId, command.Branch);

        foreach (var orderItem in command.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with id {orderItem.ProductId} already exists");

            order.AddOrderItem(new OrderItem(id, orderItem.ProductId, orderItem.Quantity, new Money(product.Price.Amount * orderItem.Quantity, Currencies.Real.ToString())));
        }

        //Persist Order
        var createdOrder = await _orderRepository.CreateAsync(order);

        //Return Order
        var result = _mapper.Map<CreateOrderResult>(createdOrder);
        return result;
    }
}
