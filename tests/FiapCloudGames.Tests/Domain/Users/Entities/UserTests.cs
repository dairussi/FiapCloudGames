using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.Entities
{
    public class UserTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenEmailInvalid()
        {
            // Arrange
            var password = RawPassword.Create("SenhaForte1!");

            // Act
            Action act = () => User.Create(
                FullName.Create("Ana Rios"),
                EmailAddress.Create("email-invalido"), // <-- mover pra dentro
                NickName.Create("aninha"),
                password.Password,
                password.Password,
                EUserRole.User
            );

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Email inválido: precisa conter @.");
        }

        [Fact]
        public void Create_ShouldReturnUser_WhenEmailAndPasswordValid()
        {
            // Arrange
            var email = EmailAddress.Create("ana@email.com");
            var password = RawPassword.Create("SenhaForte1!");

            // Act
            var user = User.Create(
                FullName.Create("Ana Rios"),
                email,
                NickName.Create("aninha"),
                password.Password, 
                password.Password,
                EUserRole.User
            );

            // Assert
            user.Should().NotBeNull();
            user.Email.Should().Be(email);
        }
    }
}
