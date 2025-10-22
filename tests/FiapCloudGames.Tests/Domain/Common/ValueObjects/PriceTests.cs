using FiapCloudGames.Domain.Common.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Tests.Domain.Common.ValueObjects
{
    public class PriceTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenPriceIsNegative()
        {
            // Arrange
            decimal negativePrice = -50m;

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
            decimal validPrice = 99.99m;

            // Act
            var price = Price.Create(validPrice);

            // Assert
            price.Should().NotBeNull();
            price.Value.Should().Be(validPrice);
        }
    }
}
