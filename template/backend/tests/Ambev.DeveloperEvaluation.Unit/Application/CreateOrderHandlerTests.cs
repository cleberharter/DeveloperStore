using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Model;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateOrderHandler _handler;
    private readonly Faker _faker;

    public CreateOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateOrderHandler(_orderRepository, _productRepository, _mapper);
        _faker = new Faker("pt_BR");
    }

    /// <summary>
    /// Tests that a valid user creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When creating order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var product = new Product(Guid.NewGuid(), _faker.Name.FirstName(Name.Gender.Female), new Money(500));
        var command = CreateOrderHandlerTestData.GenerateValidCommand();
        var order = new Order(Guid.NewGuid(), "18df26ad-e70e-4162-8be4-d346e47afc7a", "Iguatemi Store");

        command.OrderItems.ForEach(x => order.AddOrderItem(new OrderItem(order.Id, product.Id, x.Quantity, new Money(500))));

        var result = new CreateOrderResult { Id = order.Id };
        _mapper.Map<CreateOrderResult>(order).Returns(result);

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(product);
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        var createOrderResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createOrderResult.Should().NotBeNull();
        createOrderResult.Id.Should().Be(order.Id);
        await _orderRepository.Received(1).CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid order creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid order data When creating Order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateOrderCommand(); // Empty list order items in command will fail validation
        command.OrderItems = new List<OrderItemCommand>();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
