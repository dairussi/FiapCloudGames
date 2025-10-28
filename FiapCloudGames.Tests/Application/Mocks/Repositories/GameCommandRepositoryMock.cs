using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;
using Moq;

namespace FiapCloudGames.Tests.Application.Mocks.Repositories
{
    public static class GameCommandRepositoryMock
    {
        public static Mock<IGameCommandRepository> GetMock()
        {
            var mock = new Mock<IGameCommandRepository>();

            //1.: Caminho feliz da persistência
            mock.Setup(r => r.AddAsync(
                It.IsAny<Game>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((Game game, CancellationToken cancellationToken) => game);

            // 2.: Configurar GameExistsAsync para SIMULAR que o Jogo NÃO EXISTE (retorna false)
            // Isso força o Handler a seguir o caminho da ADIÇÃO.
            mock.Setup(r => r.GameExistsAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // 3.: Configurar GetByIdAsync para SIMULAR que o Jogo NÃO FOI ENCONTRADO (retorna null)
            // Isso também garante que o Handler vá para a adição se for a lógica dele
            mock.Setup(r => r.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((Game)null);

            return mock;

        }
    }
}
