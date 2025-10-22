using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Domain.Users.ValueObjects;
using FluentAssertions;
using Moq;

namespace FiapCloudGames.Tests.Application.Users.UseCases.Commands.AddOrUpdateUser
{
    public class AddOrUpdateUserCommandHandlerTests
    {
        private readonly Mock<IHashHelper> _hashHelperMock;
        private readonly Mock<IUserCommandRepository> _userRepositoryMock;
        private readonly AddOrUpdateUserCommandHandler _handler;

        public AddOrUpdateUserCommandHandlerTests()
        {
            _hashHelperMock = new Mock<IHashHelper>();
            _userRepositoryMock = new Mock<IUserCommandRepository>();

            _handler = new AddOrUpdateUserCommandHandler(_hashHelperMock.Object, _userRepositoryMock.Object);
        }
        /*
             * Nome: "Handle_ShouldAddUser_WhenUserDoesNotExist"
             * Aqui testamos o método Handle() para garantir que, 
             * quando o usuário não existir, o sistema chame AddAsync().
             * 
             * O teste cria mocks para o hash e repositório, 
             * define que o usuário não existe, executa o handler e valida:
             * - Que o resultado é sucesso (ResultData.Success = true)
             * - Que AddAsync() foi chamado 1 vez
             * - Que Update() nunca foi chamado
         */
        [Fact]
        public async Task Handle_ShouldAddUser_WhenUserDoesNotExist()
        {
            // Arrange
            var command = AddOrUpdateUserCommand.Create(
                publicId: null,
                fullName: FullName.Create("Ana Rios"),
                email: EmailAddress.Create("ana@email.com"),
                nickName: NickName.Create("aninha"),
                password: RawPassword.Create("antigaSenha!"),
                role: EUserRole.User
            );

            _hashHelperMock.Setup(h => h.GenerateHash(It.IsAny<RawPassword>()))
                           .Returns((HashResult: "hashed", Salt: "salt"));

            _userRepositoryMock.Setup(r => r.UserExistsAsync(It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeOfType<User>();
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        /*
         * Nome: "Handle_ShouldUpdateUser_WhenUserAlreadyExists"
         * Testa que, quando o usuário já existe, o método Update() é chamado em vez de AddAsync().
         * O mock do repositório retorna "true" para UserExistsAsync(),
         * e após executar o Handle(), verificamos que Update() foi chamado exatamente uma vez.
         */
        [Fact]
        public async Task Handle_ShouldUpdateUser_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = AddOrUpdateUserCommand.Create(
                publicId: Guid.NewGuid(),
                fullName: FullName.Create("Ana Atualizada"),
                email: EmailAddress.Create("ana@newemail.com"),
                nickName: NickName.Create("ana_upd"),
                password: RawPassword.Create("novaSenha!"),
                role: EUserRole.Admin
            );


            _hashHelperMock.Setup(h => h.GenerateHash(It.IsAny<RawPassword>()))
                           .Returns((HashResult: "hashed", Salt: "salt"));

            _userRepositoryMock.Setup(r => r.UserExistsAsync(existingUser.PublicId, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(existingUser, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeOfType<User>();
            _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}
