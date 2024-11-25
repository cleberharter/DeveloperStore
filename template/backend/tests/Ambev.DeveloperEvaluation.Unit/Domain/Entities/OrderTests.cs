using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class OrderTests
{
    /// <summary>
    /// Tests that when an order is active, its status changes to canceled.
    /// </summary>
    [Fact(DisplayName = "Order status should change to canceled when active")]
    public void Given_SuspendedUser_When_Activated_Then_StatusShouldBeCanceled()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();

        // Act
        order.Cancel();

        // Assert
        Assert.Equal(OrderStatus.Canceled, order.Status);
    }

    /// <summary>
    /// Tests that validation passes when all order properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid Order data")]
    public void Given_ValidOrderData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var user = OrderTestData.GenerateValidOrder();

        // Act
        var result = user.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests validation passing when the order items property is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should not pass for invalid Order data")]
    public void Given_ValidItemOrderData_When_Invalidated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var user = OrderTestData.GenerateInvalidItemsOrder();

        // Act
        var result = user.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
