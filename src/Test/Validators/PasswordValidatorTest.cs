﻿using Business.Validators;
using Business.Validators.Exceptions;

namespace Test.Validators;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("Password123!")]
    [InlineData("Str0ngP@ssword")]
    [InlineData("SecurePass12#")]
    public void Validate_ValidPassword_DoesNotThrowException(string password)
    {
        // Arrange & Act
        Action validateAction = () => PasswordValidator.Validate(password);

        validateAction();
    }

    [Theory]
    [InlineData("weak")]
    [InlineData("NoSpecialCharacter1")]
    [InlineData("OnlyLowerCaseletters")]
    [InlineData("ONLYUPPERCASELETTERS")]
    [InlineData("12345678")]
    [InlineData("Short!1")]
    [InlineData("LongPasswordWithNoSpecialCharacter123456")]
    public void Validate_InvalidPassword_ThrowsInvalidPasswordException(string password)
    {
        // Arrange & Act
        Action validateAction = () => PasswordValidator.Validate(password);

        // Assert
        Assert.Throws<InvalidPasswordException>(validateAction);
    }
}