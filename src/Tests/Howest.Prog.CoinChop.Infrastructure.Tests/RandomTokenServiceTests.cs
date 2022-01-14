using Howest.Prog.CoinChop.Infrastructure.Tokens;
using Xunit;

namespace Howest.Prog.CoinChop.Infrastructure.Tests
{
    public class RandomTokenServiceTests
    {
        /// <summary>
        /// Ensures the GenerateToken method in the RandomTokenService returns
        /// a token with a string length equal to the tokenLength input parameter 
        /// </summary>
        [Fact]
        public void GenerateToken_ReturnsTokenWithRequestedLength()
        {
            // Arrange
            var randomGenerator = new RandomTokenService();
            int expectedLength = 10;

            // Act
            var token = randomGenerator.GenerateToken(expectedLength);

            // Assert
            Assert.Equal(expectedLength, token.Length);
        }
    }
}
