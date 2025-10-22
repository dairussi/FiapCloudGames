using FiapCloudGames.Domain.Games.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Tests.Domain.Games.ValueObjects
{
    public class AgeRatingTests
    {
        [Theory]
        [InlineData("LIVRE", "Livre", 0)]
        [InlineData("FREE", "Livre", 0)]
        [InlineData("10+", "10+", 10)]
        [InlineData("12+", "12+", 12)]
        [InlineData("14+", "14+", 14)]
        [InlineData("16+", "16+", 16)]
        [InlineData("18+", "18+", 18)]
        public void Create_ShouldReturnAgeRating_WhenValidInput(string input, string expectedRating, int expectedMinAge)
        {
            // Act
            var ageRating = AgeRating.Create(input);

            // Assert
            ageRating.Should().NotBeNull();
            ageRating.Rating.Should().Be(expectedRating);
            ageRating.MinimiumAge.Should().Be(expectedMinAge);
        }

        [Theory]
        [InlineData("20+")]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData(null)]
        public void Create_ShouldThrowException_WhenInvalidInput(string input)
        {
            // Act
            Action act = () => AgeRating.Create(input!);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Classificação etária inválida.");
        }
    }
}
