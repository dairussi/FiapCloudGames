using Bogus;
using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.ValueObjects;
using FiapCloudGames.Tests.Fakers;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Users.Entities
{
    public class UserTests
    {
        private readonly Faker _faker;
        private readonly string _fullName;
        private readonly string _email;
        private readonly string _nickName;
        private readonly RawPassword _password;

        public UserTests()
        {
            _faker = new Faker("pt_BR");
            _fullName = _faker.Name.FullName();
            _email = _faker.Internet.Email();
            _nickName = _faker.Internet.UserName();
            _password = PasswordFaker.Generate();
        }


        [Fact]
        public void Create_ShouldThrowException_WhenEmailInvalid()
        {
            // Arrange
            var invalidEmail = "email-invalido";
            var password = RawPassword.Create("SenhaForte1!");

            // Act
            Action act = () => User.Create(
                FullName.Create(_fullName),
                EmailAddress.Create(invalidEmail),
                NickName.Create(_nickName),
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

            // Act
            var user = User.Create(
                FullName.Create(_fullName),
                EmailAddress.Create(_email),
                NickName.Create(_nickName),
                _password.Password, 
                _password.Password, 
                EUserRole.User
            );

            // Assert
            user.Should().NotBeNull();
            user.Email.Email.Should().Be(_email);
        }
    }
}
