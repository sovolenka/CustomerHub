using Business.Validators;
using Business.Validators.Exceptions;

namespace Test.Validators;

public class EmailValidatorTest
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user123@gmail.com")]
    [InlineData("john.doe@company.co")]
    public void Validate_ValidEmail_DoesNotThrowException(string email)
    {
        Action ValidateAction = () => EmailValidator.Validate(email);
        ValidateAction();
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("missing-at-sign.com")]
    public void Validate_InvalidEmail_ThrowsInvalidEmailException(string email)
    {
        Action validateAction = () => EmailValidator.Validate(email);
        
        Assert.Throws<InvalidEmailException>(validateAction);
    }
}