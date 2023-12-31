﻿namespace Business.Validators.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message) : base(message)
    {
    }

    public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidEmailException()
    {
    }
}