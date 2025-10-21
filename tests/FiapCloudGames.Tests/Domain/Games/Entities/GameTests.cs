using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Games.Entities
{
    public class GameTests
    {
        [Fact]
        public void Create_ShouldReturnGame_WithCorrectProperties()
        {
            // Arrange
            var description = "Jogo Educacional";
            var genre = GameGenreEnum.ActionRPG;
            var releaseDate = DateTime.UtcNow;
            var developer = "FIAP Dev";
            var price = new Price(59.99m);
            var ageRating = new AgeRating("16+", 16);
            var createdBy = 1;

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
                "Original Game",
                GameGenreEnum.RPG,
                DateTime.UtcNow.AddDays(-1),
                "FIAP Dev",
                new Price(49.99m),
                new AgeRating("12+", 12),
                1
            );

            var newDescription = "Jogo Atualizado";
            var newGenre = GameGenreEnum.ActionRPG;
            var newReleaseDate = DateTime.UtcNow;
            var newDeveloper = "FIAP Dev Updated";
            var newPrice = new Price(79.99m);
            var newAgeRating = new AgeRating("16+", 16);

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
                "Game 1",
                GameGenreEnum.RPG,
                DateTime.UtcNow,
                "Dev1",
                new Price(10),
                new AgeRating("12+", 12),
                1
            );
            var game2 = Game.Create(
                "Game 2",
                GameGenreEnum.ActionRPG,
                DateTime.UtcNow,
                "Dev2",
                new Price(20),
                new AgeRating("16+", 16),
                2
            );

            // Assert
            game1.PublicId.Should().NotBe(game2.PublicId);
        }
    }
}
