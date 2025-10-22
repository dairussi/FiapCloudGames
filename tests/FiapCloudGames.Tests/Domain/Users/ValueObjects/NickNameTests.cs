using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Tests.Domain.Users.ValueObjects
{
    public class NickNameTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenNickNameIsEmpty()
        {
            // Arrange
            string emptyNick = "";

            // Act
            Action act = () => NickName.Create(emptyNick);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*não pode ser vazio*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNickNameExceedsMaxLength()
        {
            // Arrange
            string longNick = new string('a', 101); // 101 caracteres

            // Act
            Action act = () => NickName.Create(longNick);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*máximo 100 caracteres*");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNickNameContainsSpaces()
        {
            // Arrange
            string nickWithSpaces = "Ana Rios";

            // Act
            Action act = () => NickName.Create(nickWithSpaces);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*não deve conter espaços*");
        }

        [Fact]
        public void Create_ShouldReturnNickName_WhenNickNameIsValid()
        {
            // Arrange
            string validNick = "Aninha123";

            // Act
            var nick = NickName.Create(validNick);

            // Assert
            nick.Should().NotBeNull();
            nick.Nick.Should().Be(validNick);
            nick.ToString().Should().Be(validNick);
        }
    }
}
