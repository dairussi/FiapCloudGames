using Bogus;
using FiapCloudGames.Application.Common.Outputs;
using FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.GamePurchases.Ports;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Games.ValueObjects;
using FiapCloudGames.Domain.Promotions.Ports;
using FluentAssertions;
using Moq;

namespace FiapCloudGames.Tests.Application.GamePurchases.UseCases.Commands.AddGamePurchase
{
    public class AddGamePurchasesCommandHandlerTests
    {
        private readonly Mock<IGamePurchaseCommandRepository> _gamePurchaseCommandRepositoryMock;
        private readonly Mock<IGameQueryRepository> _gameQueryRepositoryMock;
        private readonly Mock<IPromotionService> _promotionServiceMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly AddGamePurchasesCommandHandler _handler;
        private readonly Faker _faker;
        private readonly string _name;
        private readonly string _description;
        private readonly string _developer;
        private readonly decimal _priceValue;
        private readonly int _createdBy;
        private readonly int _userId;
        private readonly DateTime _baseDate;

        public AddGamePurchasesCommandHandlerTests()
        {
            _gamePurchaseCommandRepositoryMock = new Mock<IGamePurchaseCommandRepository>();
            _gameQueryRepositoryMock = new Mock<IGameQueryRepository>();
            _promotionServiceMock = new Mock<IPromotionService>();
            _userContextMock = new Mock<IUserContext>();

            _handler = new AddGamePurchasesCommandHandler(
                _gamePurchaseCommandRepositoryMock.Object,
                _gameQueryRepositoryMock.Object,
                _promotionServiceMock.Object,
                _userContextMock.Object
            );

            _faker = new Faker("pt_BR");
            _description = _faker.Commerce.ProductName();
            _developer = _faker.Company.CompanyName();
            _priceValue = _faker.Random.Decimal(50, 300);
            _createdBy = _faker.Random.Int(1, 10);
            _userId = _faker.Random.Int(1, 10);
            _baseDate = DateTime.UtcNow;
        }

        [Fact]
        public async Task Handle_ShouldCreateGamePurchase_WithCorrectFinalPrice()
        {
            // Cenário: Criar uma compra de jogo com promoção válida
            var userId = _userId;
            var gameId = Guid.NewGuid();
            var game = Game.Create(
                name: _name,
                description: _description,
                genre: GameGenreEnum.RPG,
                releaseDate: _baseDate,
                developer: _developer,
                price: Price.Create(100M),
                ageRating: AgeRating.Create("16+")
            );

            _userContextMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _gameQueryRepositoryMock.Setup(x => x.GetByIdAsync(gameId, It.IsAny<CancellationToken>())).ReturnsAsync(game);
            _promotionServiceMock.Setup(x => x.GetBestDiscountAsync(game.Price, gameId, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PromotionServiceResult(1, Price.Create(20)));
            _gamePurchaseCommandRepositoryMock.Setup(x => x.AddAsync(It.IsAny<GamePurchase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GamePurchase gp, CancellationToken ct) =>
                {
                    var prop = typeof(GamePurchase).GetProperty("Game", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    prop!.SetValue(gp, game);
                    return gp;
                });

            var command = new AddGamePurchasesComand(gameId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Data.FinalPrice.Should().Be(80); // 100 - 20
            result.Data.PromotionValue!.Value.Should().Be(20);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenGameDoesNotExist()
        {
            var gameId = Guid.NewGuid();

            _userContextMock.Setup(x => x.GetCurrentUserId()).Returns(1);
            _gameQueryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Game?)null);

            var command = new AddGamePurchasesComand(gameId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Jogo não encontrado.");
        }

        [Fact]
        public async Task Handle_ShouldApplyPromotionCorrectly()
        {
            var userId = _userId;
            var gameId = Guid.NewGuid();
            var game = Game.Create(
                name: _name,
                description: _description,
                genre: GameGenreEnum.ActionRPG,
                releaseDate: _baseDate,
                developer: _developer,
                price: Price.Create(200M),
                ageRating: AgeRating.Create("16+")
            );

            _userContextMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _gameQueryRepositoryMock.Setup(x => x.GetByIdAsync(gameId, It.IsAny<CancellationToken>())).ReturnsAsync(game);
            _promotionServiceMock.Setup(x => x.GetBestDiscountAsync(game.Price, gameId, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PromotionServiceResult(5, Price.Create(50)));
            _gamePurchaseCommandRepositoryMock.Setup(x => x.AddAsync(It.IsAny<GamePurchase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GamePurchase gp, CancellationToken ct) =>
                {
                    var prop = typeof(GamePurchase).GetProperty("Game", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    prop!.SetValue(gp, game);
                    return gp;
                });

            var command = new AddGamePurchasesComand(gameId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Data.FinalPrice.Should().Be(150); // 200 - 50
            result.Data.PromotionValue!.Value.Should().Be(50);
        }

        [Fact]
        public async Task Handle_ShouldCreateGamePurchase_WhenNoPromotionAvailable()
        {
            // Cenário: Criar uma compra de jogo quando não há promoção aplicada
            var userId = 1;
            var gameId = Guid.NewGuid();
            var game = Game.Create(
                name: _name,
                description: _description,
                genre: GameGenreEnum.RPG,
                releaseDate: _baseDate,
                developer: _developer,
                price: Price.Create(120M),
                ageRating: AgeRating.Create("16+")
            );

            _userContextMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _gameQueryRepositoryMock.Setup(x => x.GetByIdAsync(gameId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(game);

            // Retorna promoção zerada
            _promotionServiceMock.Setup(x => x.GetBestDiscountAsync(game.Price, gameId, userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PromotionServiceResult(null, Price.Create(0m)));

            _gamePurchaseCommandRepositoryMock.Setup(x => x.AddAsync(It.IsAny<GamePurchase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GamePurchase gp, CancellationToken ct) =>
                {
                    var prop = typeof(GamePurchase).GetProperty("Game", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    prop!.SetValue(gp, game);
                    return gp;
                });

            var command = new AddGamePurchasesComand(gameId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Data.FinalPrice.Should().Be(120); // Sem desconto
            result.Data.PromotionValue!.Value.Should().Be(0); // Promoção zerada
        }

    }
}

