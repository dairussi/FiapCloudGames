using FiapCloudGames.Domain.Common.Ports;
using System.Security.Cryptography;

namespace FiapCloudGames.Infraestructure.Adapters;

public class HashHelper : IHashHelper
{
    public (string Hash, string Salt) GenerateHash(string password)
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(saltBytes);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }
    }
}
