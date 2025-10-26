namespace FiapCloudGames.Domain.Users.ValueObjects;

public class FullName
{
    private FullName(string name)
    {
        Name = name;
    }
    public string Name { get; set; } = default!;
    public static FullName Create(string rawInput)
    {
        if (string.IsNullOrEmpty(rawInput))
            throw new ArgumentException("Nome não pode ser vazio.");

        if (rawInput.Length > 100)
            throw new ArgumentException("Nome deve ter no máximo 100 caracteres.");

        if (!rawInput.Contains(" "))
            throw new ArgumentException("Nome completo deve conter nome e sobrenome.");

        return new FullName(rawInput);
    }

}