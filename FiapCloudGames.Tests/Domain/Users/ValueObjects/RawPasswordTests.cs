using Bogus;
using FiapCloudGames.Domain.Users.ValueObjects;
using FiapCloudGames.Tests.Fakers;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class RawPasswordTests
    {
        private readonly Faker _faker;
        private readonly RawPassword _password;

        public RawPasswordTests()
        {
            _faker = new Faker("pt_BR");
            _password = PasswordFaker.Generate();
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordEmpty()
        {
            // Arrange
            string emptyPassword = string.Empty; // senha vazia

            // Act
            Action act = () => RawPassword.Create(emptyPassword);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Senha não pode ser vazia.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordWeak()
        {
            // Arrange
            string weakPassword = _faker.Random.String2(5, _faker.Random.Word());

            // Act
            Action act = () => RawPassword.Create(weakPassword);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 8 caracteres.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordHasNoUppercase()
        {
            string weakPassword = _faker.Random.String2(10, _faker.Random.Word().ToLower()); // sem letra maiúscula
            Action act = () => RawPassword.Create(weakPassword);
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 1 letra maiúscula.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordHasNoSpecialChar()
        {
            string weakPassword = _faker.Random.String2(10, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");  // sem caracter especial
            Action act = () => RawPassword.Create(weakPassword);
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 1 caracter especial.");
        }

        [Fact]
        public void Create_ShouldReturnRawPassword_WhenPasswordValid()
        {
            string validPassword = _password.Password;
            var rawPassword = RawPassword.Create(validPassword);

            // Certifique-se de acessar a propriedade que contém a string
            rawPassword.Password.Should().Be(validPassword);
        }

        [Fact]
        public void Create_ShouldReturnRawPassword_WhenPasswordHasExactly8Characters()
        {
            // Arrange
            var validPassword = "123456A!"; // 8 caracteres: inclui maiúscula, minúscula, número e especial

            // Act
            var rawPassword = RawPassword.Create(validPassword);

            // Assert
            rawPassword.Should().NotBeNull();
            rawPassword.Password.Should().Be(validPassword);
        }

    }
}
