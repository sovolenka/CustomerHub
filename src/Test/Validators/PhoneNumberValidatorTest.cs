﻿using Business.Validators;
using Business.Validators.Exceptions;

namespace Test.Validators;

public class PhoneNumberValidatorTest
{
    [Theory]
    [InlineData("+1234567890")]
    [InlineData("+1 234 567 890")]
    [InlineData("+123 45 67890")]
    [InlineData("+12 345 67 89 0")]
    public void Validate_ValidPhoneNumber_DoesNotThrowException(string phoneNumber)
    {
        // Arrange & Act
        Action validateAction = () => PhoneNumberValidator.Validate(phoneNumber);

        // Assert
        validateAction();
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("+")]
    [InlineData("+12345")]
    [InlineData("+12345abc")]
    [InlineData("invalidPhoneNumber")]
    public void Validate_InvalidPhoneNumber_ThrowsInvalidPhoneNumberException(string phoneNumber)
    {
        // Arrange & Act
        Action validateAction = () => PhoneNumberValidator.Validate(phoneNumber);

        // Assert
        Assert.Throws<InvalidPhoneNumberException>(validateAction);
    }
}