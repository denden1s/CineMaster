using System.Security.Cryptography;
using System.Text;

namespace CineMaster_frontend.Services;

public static class Encryption
{
    public static string Hash(string plainText)
    {
        var inputBytes = Encoding.UTF8.GetBytes(plainText);
        var outputHash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(outputHash);
    }

    public static string HashPassword(string login, string password)
    {
        // Matches backend encryption: SHA256(password + login)
        return Hash(password + login);
    }
}
