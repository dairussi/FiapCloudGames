using Bogus;
using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Tests.Fakers
{
    public class PasswordFaker
    {
        private static readonly Faker _faker = new Faker("pt_BR");
        private static readonly string _lower = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string _upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string _numbers = "0123456789";
        private static readonly string _specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        public static RawPassword Generate()
        {
            // 1 minúscula
            var lower = _faker.Random.ArrayElement(_lower.ToCharArray()).ToString();
            // 1 maiúscula
            var upper = _faker.Random.ArrayElement(_upper.ToCharArray()).ToString();
            // 1 número
            var number = _faker.Random.ArrayElement(_numbers.ToCharArray()).ToString();
            // 1 caractere especial
            var special = _faker.Random.ArrayElement(_specialChars.ToCharArray()).ToString();

            // Completa o restante da senha com caracteres aleatórios
            int remainingLength = _faker.Random.Int(4, 12); // tamanho total entre 8 e 16
            var allChars = (_lower + _upper + _numbers + _specialChars).ToCharArray();
            var remaining = new string(Enumerable.Range(0, remainingLength)
                .Select(_ => _faker.Random.ArrayElement(allChars)).ToArray());

            // Embaralha todos os caracteres para não ficar previsível
            var passwordChars = (lower + upper + number + special + remaining).ToCharArray();
            passwordChars = passwordChars.OrderBy(x => _faker.Random.Int()).ToArray();

            var password = new string(passwordChars);

            return RawPassword.Create(password);
        }
    }
}
