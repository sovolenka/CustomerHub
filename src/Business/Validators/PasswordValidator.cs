﻿using System.Text.RegularExpressions;
using Business.Validators.Exceptions;

namespace Business.Validators;

public static class PasswordValidator
{
    private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

    public static void Validate(string password)
    {
        if (!Regex.IsMatch(password, PasswordRegex))
        {
            throw new InvalidPasswordException("Password must be between 8 and 15 characters" +
                                               "and contain at least one lowercase letter, one uppercase letter," +
                                               "one numeric digit, and one special character.");
        }
    }
}