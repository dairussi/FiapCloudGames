namespace FiapCloudGames.Domain.Common.Ports;
public interface IHashHelper
{
    public (string Hash, string Salt) GenerateHash(string password);
}
