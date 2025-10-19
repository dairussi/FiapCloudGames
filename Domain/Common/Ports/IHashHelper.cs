using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Domain.Common.Ports;

public interface IHashHelper
{
    public (string Hash, string Salt) GenerateHash(RawPassword password);
    bool VerifyHash(RawPassword password, string hash, string salt);
}
