using System.Security.Cryptography;
using System.Text;

namespace CineMaster_backend.src.Utils;

public class Encryption
{
  public Encryption() { }
  public string Hash(string plainText)
  {
    var inputBytes = Encoding.UTF8.GetBytes(plainText);
    var outputHash = SHA256.HashData(inputBytes);
    return Convert.ToHexString(outputHash);
  }
}
