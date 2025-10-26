using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.ValueObjects;
using System.Security.Cryptography;

namespace FiapCloudGames.Infraestructure.Adapters.Common;

public class HashHelper : IHashHelper
{
    public (string Hash, string Salt) GenerateHash(RawPassword password)
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(saltBytes);

        var passwordString = password.ToString();

        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordString, saltBytes, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }
    }
    public bool VerifyHash(RawPassword password, string hash, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        var passwordString = password.ToString();

        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordString, saltBytes, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string computedHash = Convert.ToBase64String(hashBytes);

            return computedHash == hash;
        }
    }
}