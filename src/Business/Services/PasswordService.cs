using System.Security.Cryptography;
using System.Text;

namespace Business.Services;

public class PasswordService
{
    public byte[] HashString(string inputString)
    {
        using HashAlgorithm algorithm = SHA256.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
}