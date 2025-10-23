using Bogus;
using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class EmailAddressTests
    {
        private readonly Faker _faker;
        private readonly string _email;
        public EmailAddressTests()
        {
            _faker = new Faker("pt_BR");
            _email = _faker.Internet.Email();
        }

        [Fact]
        public void Create_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            string emptyEmail = string.Empty;

            // Act
            Action act = () => EmailAddress.Create(emptyEmail);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*obrigatório*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenEmailIsNull()
        {
            // Arrange
            string nullEmail = null;

            // Act
            Action act = () => EmailAddress.Create(nullEmail);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*obrigatório*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenEmailMissingAtSymbol()
        {
            // Arrange
            string invalidEmail = _faker.Internet.Email().Replace("@", "");

            // Act
            Action act = () => EmailAddress.Create(invalidEmail);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*precisa conter @*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenEmailMissingDotAfterAt()
        {
            // Arrange
            string invalidEmail = _faker.Internet.Email().Replace(".", "");

            // Act
            Action act = () => EmailAddress.Create(invalidEmail);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*precisa conter um '.' após o '@'*");
        }

        [Fact]
        public void Create_ShouldReturnEmailAddress_WhenEmailValid()
        {
            // Arrange
            string validEmail = _email;

            // Act
            var emailObj = EmailAddress.Create(validEmail);

            // Assert
            emailObj.Should().NotBeNull();
            emailObj.Email.Should().Be(validEmail);
            emailObj.ToString().Should().Be(validEmail);
        }

        [Fact]
        public void Create_ShouldTrimEmail_WhenEmailHasSpaces()
        {
            // Arrange
            string emailWithSpaces = $"  {_email}  ";

            // Act
            var emailObj = EmailAddress.Create(emailWithSpaces);

            // Assert
            emailObj.Email.Should().Be(_email);
        }
    }
}
