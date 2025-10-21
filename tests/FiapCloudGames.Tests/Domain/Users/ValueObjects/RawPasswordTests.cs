using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class RawPasswordTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenPasswordEmpty()
        {
            // Arrange
            string emptyPassword = ""; // senha vazia

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
            string weakPassword = "12345"; // menos de 8 caracteres

            // Act
            Action act = () => RawPassword.Create(weakPassword);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 8 caracteres.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordHasNoUppercase()
        {
            string weakPassword = "senha123!"; // sem letra maiúscula
            Action act = () => RawPassword.Create(weakPassword);
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 1 letra maiúscula.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPasswordHasNoSpecialChar()
        {
            string weakPassword = "Senha1234"; // sem caracter especial
            Action act = () => RawPassword.Create(weakPassword);
            act.Should().Throw<ArgumentException>()
               .WithMessage("A senha deve ter no mínimo 1 caracter especial.");
        }

        [Fact]
        public void Create_ShouldReturnRawPassword_WhenPasswordValid()
        {
            string validPassword = "Senha123!";
            var rawPassword = RawPassword.Create(validPassword);

            // Certifique-se de acessar a propriedade que contém a string
            rawPassword.Password.Should().Be(validPassword);
        }

        [Fact]
        public void Create_ShouldReturnRawPassword_WhenPasswordHasExactly8Characters()
        {
            // Arrange
            var validPassword = "Abc123!@"; // 8 caracteres: inclui maiúscula, minúscula, número e especial

            // Act
            var rawPassword = RawPassword.Create(validPassword);

            // Assert
            rawPassword.Should().NotBeNull();
            rawPassword.Password.Should().Be(validPassword);
        }

    }
}
