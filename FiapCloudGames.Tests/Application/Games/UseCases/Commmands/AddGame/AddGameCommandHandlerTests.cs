using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;
using FiapCloudGames.Tests.Application.Mocks.Repositories;
using Moq;

namespace FiapCloudGames.Tests.Application.Games.UseCases.Commmands.AddGame
{
    public class AddGameCommandHandlerTests
    {
        [Fact]
        public async Task GameHandle_NewGameValidCommand_ShouldCallAddAndSucceed()
        {
            var commandRepoMock = GameCommandRepositoryMock.GetMock();

            var handler = new AddOrUpdateGameCommandHandler(commandRepoMock.Object);

            var command = AddOrUpdateGameCommand.Create(
                publicId: null,
                description: "Test Game",
                genre: GameGenreEnum.ActionRPG,
                releaseDate: DateTime.UtcNow,
                developer: "Test Developer",
                price: new Price(59.99m),
                ageRating: new AgeRating("18+",18),
                createdBy: 1
            );
            var cancellationToken = CancellationToken.None;

            var result = await handler.Handle(command, cancellationToken);

            commandRepoMock.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GameHandle_CommandWithFutureYear_ShouldNotCallRepositoryAndReturnFailure()
        {
            // ARRANGE
            var commandRepoMock = GameCommandRepositoryMock.GetMock();
            var handler = new AddOrUpdateGameCommandHandler(commandRepoMock.Object);

            // Comando inválido: ano de lançamento futuro
            var command = AddOrUpdateGameCommand.Create(
                publicId: Guid.NewGuid(),
                description: "Test Game Future Year",
                genre: GameGenreEnum.Horror,
                releaseDate: new DateTime(2035, 10, 1),
                developer: "Future Developer",
                price: new Price(89.99m),
                ageRating: new AgeRating("10+", 10),
                createdBy: 1
            );
            // ACT
            var result = await handler.Handle(command, CancellationToken.None);

            // ASSERT
            // 1. O Repositório de Comando (Mock) NÃO deve ser chamado.
            commandRepoMock.Verify(
                r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()),
                Times.Never,
                "O Repositório não deve ser chamado se o Command for inválido."
            );

            // 2. O resultado deve ser falha (ou conter o erro).
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GameHandle_ExistingGameValidCommand_ShouldCallUpdateAsync()
        {
            var commandRepoMock = GameCommandRepositoryMock.GetMock();

            // 1. Simular que o jogo JÁ EXISTE e que GetByIdAsync o retorna
            var existingGame = Game.Create(
                description: "Batman",
                genre: GameGenreEnum.RPG,
                releaseDate: new DateTime(2025,10,01,00,26,10,740),
                developer: "EA GAMES",
                price: new Price(1890.00M),
                ageRating: new AgeRating("10+", 10),
                createdBy: 1
            );

            var existingGameId = existingGame.PublicId;
            commandRepoMock.Setup(r => r.GetByIdAsync(existingGameId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingGame);

            // 2. Mockar o método de atualização
            commandRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Game game, CancellationToken ct) => game);

            var handler = new AddOrUpdateGameCommandHandler(commandRepoMock.Object);

            // 3. Command com o ID existente
            var command = AddOrUpdateGameCommand.Create(
                publicId: existingGameId,
                description: "Batman",
                genre: GameGenreEnum.RPG,
                releaseDate: new DateTime(2025, 10, 01, 00, 26, 10, 740),
                developer: "EA GAMES",
                price: new Price(1890.00M),
                ageRating: new AgeRating("10+", 10),
                createdBy: 1
            );

            var result = await handler.Handle(command, CancellationToken.None);

            // VERIFY: Verificar que o UpdateAsync foi chamado, e NÃO o AddAsync
            commandRepoMock.Verify(
                r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()),
                Times.Once);

            commandRepoMock.Verify(
                r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
