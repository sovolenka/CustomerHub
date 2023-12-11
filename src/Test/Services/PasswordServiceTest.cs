using Business.Services;

namespace Test.Services;

public class PasswordServiceTest
{
    PasswordService _passwordService = new();

    [Fact]
    public void HashString_WithValidInput_ReturnsNonEmptyByteArray()
    {
        var inputString = "testPassword";

        var hashedBytes = _passwordService.HashString(inputString);

        Assert.NotNull(hashedBytes);
        Assert.NotEmpty(hashedBytes);
    }

    [Fact]
    public void HashString_WithSameInput_ReturnsSameHash()
    {
        var inputString = "testPassword";

        var hash1 = _passwordService.HashString(inputString);
        var hash2 = _passwordService.HashString(inputString);

        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void HashString_WithDifferentInput_ReturnsDifferentHash()
    {
        var inputString1 = "testPassword1";
        var inputString2 = "testPassword2";

        var hash1 = _passwordService.HashString(inputString1);
        var hash2 = _passwordService.HashString(inputString2);

        Assert.NotEqual(hash1, hash2);
    }
}