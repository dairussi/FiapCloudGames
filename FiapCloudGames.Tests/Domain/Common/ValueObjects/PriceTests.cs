using Bogus;
using FiapCloudGames.Domain.Common.ValueObjects;
using FluentAssertions;

namespace FiapCloudGames.Tests.Domain.Common.ValueObjects
{
    public class PriceTests
    {
        private readonly Faker _faker;

        public PriceTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenPriceIsNegative()
        {
            // Arrange
            decimal negativePrice = _faker.Random.Decimal(-1000, -0.01m);

            // Act
            Action act = () => Price.Create(negativePrice);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("O preço não pode ser negativo");
        }

        [Fact]
        public void Create_ShouldReturnPrice_WhenValueIsValid()
        {
            // Arrange
            decimal validPrice = _faker.Random.Decimal(0.01m, 10000m);

            // Act
            var price = Price.Create(validPrice);

            // Assert
            price.Should().NotBeNull();
            price.Value.Should().Be(validPrice);
        }
    }
}
