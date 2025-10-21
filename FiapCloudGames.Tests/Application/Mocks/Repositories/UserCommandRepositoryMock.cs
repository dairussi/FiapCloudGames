using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;
using Moq;

namespace FiapCloudGames.Tests.Application.Mocks.Repositories
{
    public static class UserCommandRepositoryMock
    {
        public static Mock<IUserCommandRepository> GetMock()
        {
            var mock = new Mock<IUserCommandRepository>();

            //1.: Caminho feliz da persistência
            mock.Setup(r => r.AddAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((User user, CancellationToken cancellationToken) => user);

            mock.Setup(r => r.UserExistsAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mock.Setup(r => r.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            return mock;

        }
    }
}
