namespace FiapCloudGames.Domain.Users.ValueObjects;

public class NickName
{
    private NickName(string nick)
    {
        Nick = nick;
    }
    public string Nick { get; set; } = default!;
    public static NickName Create(string rawNickName)
    {
        if (string.IsNullOrEmpty(rawNickName))
            throw new ArgumentException("Nick name não pode ser vazio.");

        if (rawNickName.Length > 100)
            throw new ArgumentException("Nick name deve ter no máximo 100 caracteres.");

        if (rawNickName.Contains(" "))
            throw new ArgumentException("Nick name não deve conter espaços.");

        return new NickName(rawNickName);
    }
    public override string ToString() => Nick;
}
