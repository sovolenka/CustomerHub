using System.Text.RegularExpressions;
using Business.Validators.Exceptions;

namespace Business.Validators
{
    public static class PhoneNumberValidator
    {
        private const string PhoneNumberRegex = @"^\+(?:[0-9] ?){6,14}[0-9]$";

        public static void Validate(string phoneNumber)
        {
            if (!Regex.IsMatch(phoneNumber, PhoneNumberRegex))
            {
                throw new InvalidPhoneNumberException("Phone number must be a valid international phone number.");
            }
        }
    }
}
