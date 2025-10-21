using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Games.ValueObjects;
using FluentAssertions;
using Moq;

namespace FiapCloudGames.Tests.Application.Games.UseCases.Commmands.AddGame
{
    public class AddOrUpdateGameCommandHandlerTests
    {
        private readonly Mock<IGameCommandRepository> _gameRepositoryMock;
        private readonly AddOrUpdateGameCommandHandler _handler;

        public AddOrUpdateGameCommandHandlerTests()
        {
            _gameRepositoryMock = new Mock<IGameCommandRepository>();
            _handler = new AddOrUpdateGameCommandHandler(_gameRepositoryMock.Object);
        }

        /*
         * Nome: Handle_ShouldAddGame_WhenGameDoesNotExist
         * 
         * Testa que, quando o jogo não existir (GameExistsAsync retorna false),
         * o handler chama AddAsync() e não chama Update().
         */
        [Fact]
        public async Task Handle_ShouldAddGame_WhenGameDoesNotExist()
        {
            // Arrange
            var command = AddOrUpdateGameCommand.Create(
                publicId: null,
                description: "Jogo de ação e aventura",
                genre: GameGenreEnum.ActionRPG,
                releaseDate: DateTime.UtcNow,
                developer: "Test Developer",
                price: new Price(59.99m),
                ageRating: new AgeRating("18+", 18),
                createdBy: 1
            );

            _gameRepositoryMock
                .Setup(r => r.GameExistsAsync(It.IsAny<Guid?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeOfType<Game>();

            _gameRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
            _gameRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        /*
         * Nome: Handle_ShouldUpdateGame_WhenGameAlreadyExists
         * 
         * Testa que, quando o jogo já existir (GameExistsAsync retorna true),
         * o handler chama Update() e não chama AddAsync().
         */
        [Fact]
        public async Task Handle_ShouldUpdateGame_WhenGameAlreadyExists()
        {
            // Arrange
            var existingGameId = Guid.NewGuid();
            var existingGameCommand = AddOrUpdateGameCommand.Create(
                publicId: existingGameId,
                description: "Versão atualizada do jogo",
                genre: GameGenreEnum.ActionRPG,
                releaseDate: DateTime.UtcNow,
                developer: "Updated Developer",
                price: new Price(79.99m),
                ageRating: new AgeRating("16+", 16),
                createdBy: 1
            );

            var existingGameEntity = Game.Create(
                description: "Jogo original",
                genre: GameGenreEnum.ActionRPG,
                releaseDate: DateTime.UtcNow.AddDays(-1),
                developer: "Updated Developer",
                price: new Price(59.99m),
                ageRating: new AgeRating("16+", 16),
                createdBy: 1
            );

            // Setup do GameExistsAsync para retornar false, porque o ID já existe
            _gameRepositoryMock
                .Setup(r => r.GameExistsAsync(
                    existingGameCommand.PublicId,
                    existingGameCommand.Description,
                    existingGameCommand.Developer,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false); // falso porque o update é permitido se não existir duplicado

            // Setup do GetByIdAsync para retornar a entidade existente
            _gameRepositoryMock
                .Setup(r => r.GetByIdAsync(existingGameId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingGameEntity);

            // Setup do UpdateAsync
            _gameRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Game g, CancellationToken _) => g);

            // Act
            var result = await _handler.Handle(existingGameCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeOfType<Game>();

            _gameRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
            _gameRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
