using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class EmailAddressTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            string emptyEmail = "";

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
            string invalidEmail = "testeemail.com";

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
            string invalidEmail = "teste@com";

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
            string validEmail = "ana@example.com";

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
            string emailWithSpaces = "  ana@example.com  ";

            // Act
            var emailObj = EmailAddress.Create(emailWithSpaces);

            // Assert
            emailObj.Email.Should().Be("ana@example.com");
        }
    }
}
