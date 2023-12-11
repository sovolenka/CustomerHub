﻿namespace Business.Validators.Exceptions
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string message) : base(message)
        {
        }

        public InvalidPhoneNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidPhoneNumberException()
        {
        }
    }
}
