using Bogus;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Games.Entities
{
    public class GameTests
    {
        private readonly Faker _faker;
        private readonly string _description;
        private readonly string _developer;
        private readonly decimal _priceValue;
        private readonly int _createdBy;
        private readonly DateTime _baseDate;

        public GameTests()
        {
            _faker = new Faker("pt_BR");
            _description = _faker.Commerce.ProductName();
            _developer = _faker.Company.CompanyName();
            _priceValue = _faker.Random.Decimal(50, 300);
            _createdBy = _faker.Random.Int(1, 10);
            _baseDate = DateTime.UtcNow;
        }


        [Fact]
        public void Create_ShouldReturnGame_WithCorrectProperties()
        {
            // Arrange
            var description = _description;
            var genre = GameGenreEnum.ActionRPG;
            var releaseDate = _baseDate;
            var developer = _developer;
            var price = new Price(_priceValue);
            var ageRating = new AgeRating("16+", 16);
            var createdBy = _createdBy;

            // Act
            var game = Game.Create(description, genre, releaseDate, developer, price, ageRating, createdBy);

            // Assert
            game.Should().NotBeNull();
            game.Description.Should().Be(description);
            game.Genre.Should().Be(genre);
            game.ReleaseDate.Should().Be(releaseDate);
            game.Developer.Should().Be(developer);
            game.Price.Should().Be(price);
            game.AgeRating.Should().Be(ageRating);
            game.CreatedBy.Should().Be(createdBy);
            game.PublicId.Should().NotBeEmpty();
            game.Promotions.Should().BeEmpty();
        }

        [Fact]
        public void UpdateDetails_ShouldModifyGamePropertiesCorrectly()
        {
            // Arrange
            var game = Game.Create(
                _description,
                GameGenreEnum.RPG,
                _baseDate.AddDays(-1),
                _developer,
                new Price(_priceValue),
                new AgeRating("12+", 12),
                _createdBy
            );

            var newDescription = _description;
            var newGenre = GameGenreEnum.Racing;
            var newReleaseDate = _baseDate;
            var newDeveloper = _developer;
            var newPrice = new Price(_priceValue);
            var newAgeRating = new AgeRating("10+", 10);

            // Act
            game.UpdateDetails(newDescription, newGenre, newReleaseDate, newDeveloper, newPrice, newAgeRating);

            // Assert
            game.Description.Should().Be(newDescription);
            game.Genre.Should().Be(newGenre);
            game.ReleaseDate.Should().Be(newReleaseDate);
            game.Developer.Should().Be(newDeveloper);
            game.Price.Should().Be(newPrice);
            game.AgeRating.Should().Be(newAgeRating);
        }

        [Fact]
        public void Create_MultipleGames_ShouldHaveDifferentPublicIds()
        {
            // Act
            var game1 = Game.Create(
                _description,
                GameGenreEnum.RPG,
                _baseDate,
               _developer,
                new Price(_priceValue),
                new AgeRating("12+", 12),
                _createdBy
            );
            var game2 = Game.Create(
                _description,
                GameGenreEnum.ActionRPG,
                _baseDate,
                _developer,
                new Price(_priceValue),
                new AgeRating("16+", 16),
                _createdBy
            );

            // Assert
            game1.PublicId.Should().NotBe(game2.PublicId);
        }
    }
}
