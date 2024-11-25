using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class OrderValidatorTests
{
    private readonly OrderValidator _validator;
    public OrderValidatorTests()
    {
        _validator = new OrderValidator();
    }

    /// <summary>
    /// Tests that validation passes when all user properties are valid.
    /// This test verifies that a user with valid:
    /// - Username (3-50 characters)
    /// - Password (meets complexity requirements)
    /// - Email (valid format)
    /// - Phone (valid Brazilian format)
    /// - Status (Active/Suspended)
    /// - Role (Customer/Admin)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid order should pass all validation rules")]
    public void Given_Validorder_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid username formats.
    /// This test verifies that usernames that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The username is a required field and must be between 3 and 50 characters.
    /// </summary>
    /// <param name="username">The invalid username to test.</param>
    [Fact(DisplayName = "Invalid order item empty should fail validation")]
    public void Given_InvalidOrderItem_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateInvalidItemsOrder();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OrderItems);
    }
}
