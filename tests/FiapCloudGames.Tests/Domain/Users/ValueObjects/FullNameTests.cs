using Bogus;
using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class FullNameTests
    {
        private readonly Faker _faker;
        private readonly string _fullName;

        public FullNameTests()
        {
            _faker = new Faker("pt_BR");
            _fullName = _faker.Name.FullName();
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            string emptyName = string.Empty;

            // Act
            Action act = () => FullName.Create(emptyName);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*não pode ser vazio*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNameExceedsMaxLength()
        {
            // Arrange
            string longName = _faker.Random.String2(101); // 101 caracteres

            // Act
            Action act = () => FullName.Create(longName);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*máximo 100 caracteres*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNameDoesNotContainSpace()
        {
            // Arrange
            string singleName = _faker.Random.Word();

            // Act
            Action act = () => FullName.Create(singleName);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*nome completo deve conter nome e sobrenome*");
        }

        [Fact]
        public void Create_ShouldReturnFullName_WhenNameIsValid()
        {
            // Arrange
            string validName = _fullName;

            // Act
            var fullName = FullName.Create(validName);

            // Assert
            fullName.Should().NotBeNull();
            fullName.Name.Should().Be(validName);
        }
    }
}
