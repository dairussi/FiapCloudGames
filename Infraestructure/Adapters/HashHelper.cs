// FiapCloudGames.Infraestructure.Adapters/HashHelper.cs
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace FiapCloudGames.Infraestructure.Adapters;

public class HashHelper : IHashHelper
{
    public (string Hash, string Salt) GenerateHash(RawPassword password)
    {
        // 1. Usa o valor da propriedade 'Value' do objeto RawPassword
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        // 2. Aumenta o número de iterações para maior segurança
        const int iterations = 310000;

        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(saltBytes);

        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterations, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }
    }
}