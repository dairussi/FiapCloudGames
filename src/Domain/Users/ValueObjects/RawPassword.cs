namespace FiapCloudGames.Domain.Users.ValueObjects;

public record RawPassword
{
    private RawPassword(string password)
    {
        Password = password;
    }
    public string Password { get; set; } = default!;

    public static RawPassword Create(string rawInput)
    {
        if (string.IsNullOrEmpty(rawInput))
            throw new ArgumentException("Senha não pode ser vazia.");

        if (rawInput.Length < 8)
            throw new ArgumentException("A senha deve ter no mínimo 8 caracteres.");

        if (!rawInput.Any(char.IsUpper))
            throw new ArgumentException("A senha deve ter no mínimo 1 letra maiúscula.");

        if (!rawInput.Any(charItem => !char.IsLetterOrDigit(charItem)))
            throw new ArgumentException("A senha deve ter no mínimo 1 caracter especial.");

        return new RawPassword(rawInput);
    }

}
