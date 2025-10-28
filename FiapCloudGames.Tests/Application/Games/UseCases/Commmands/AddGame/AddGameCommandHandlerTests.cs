using Bogus;
using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Application.Games.Outputs;
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
        private readonly Faker _faker;
        private readonly string _description;
        private readonly string _name;
        private readonly string _developer;
        private readonly decimal _priceValue;
        private readonly int _createdBy;
        private readonly DateTime _baseDate;

        public AddOrUpdateGameCommandHandlerTests()
        {
            _gameRepositoryMock = new Mock<IGameCommandRepository>();
            _handler = new AddOrUpdateGameCommandHandler(_gameRepositoryMock.Object);
            _faker = new Faker("pt_BR");
            _name = _faker.Name.FullName();
            _description = _faker.Commerce.ProductName();
            _developer = _faker.Company.CompanyName();
            _priceValue = _faker.Random.Decimal(50, 300);
            _createdBy = _faker.Random.Int(1, 10);
            _baseDate = DateTime.UtcNow;

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
                name: _name,
                description: _description,
                genre: GameGenreEnum.ActionRPG,
                releaseDate: _baseDate,
                developer: _developer,
                price: new Price(_priceValue),
                ageRating: new AgeRating("18+", 18),
                publicId: null
            );

            _gameRepositoryMock
                .Setup(r => r.GameExistsAsync(It.IsAny<Guid?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeOfType<GameOutput>();

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
                name: _name,
                description: _description,
                genre: GameGenreEnum.ActionRPG,
                releaseDate: _baseDate,
                developer: _developer,
                price: new Price(_priceValue),
                ageRating: new AgeRating("16+", 16),
                publicId: existingGameId
            );

            var existingGameEntity = Game.Create(
                name: _name,
                description: _description,
                genre: GameGenreEnum.BattleRoyale,
                releaseDate: _baseDate.AddDays(-1),
                developer: _developer,
                price: new Price(_priceValue),
                ageRating: new AgeRating("Livre", 0)
            );

            //// Setup do GameExistsAsync para retornar false, indicando que não há outro jogo duplicado
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
            result.Data.Should().BeOfType<GameOutput>();

            _gameRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
            _gameRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
