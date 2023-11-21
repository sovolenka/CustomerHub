using System.Text.RegularExpressions;
using Business.Validators.Exceptions;

namespace Business.Validators;

public static class EmailValidator
{
    private const string EmailRegex = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";

    public static void Validate(string email)
    {
        if (!Regex.IsMatch(email, EmailRegex))
        {
            throw new InvalidEmailException("Email must be a valid email address.");
        }
    }
}