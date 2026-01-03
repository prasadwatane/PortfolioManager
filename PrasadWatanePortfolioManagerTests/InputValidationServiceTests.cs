using PrasadWatanePortfolioManager.Application.Services;
using Xunit;

namespace PrasadWatanePortfolioManager.Tests.Application.Services
{
    public class InputValidationServiceTests
    {
        private readonly InputValidationService _validationService;

        public InputValidationServiceTests()
        {
            _validationService = new InputValidationService();
        }

        [Fact]
        public void ValidateCommandLineArgs_WithValidArgs_ShouldReturnValid()
        {
            // Arrange
            string[] args = { "input.txt" };

            // Act
            var (isValid, errorMessage) = _validationService.ValidateCommandLineArgs(args);

            // Assert
            Assert.True(isValid);
            Assert.Empty(errorMessage);
        }

        [Fact]
        public void ValidateCommandLineArgs_WithMultipleArgs_ShouldReturnValid()
        {
            // Arrange
            string[] args = { "input.txt", "extra_arg" };

            // Act
            var (isValid, errorMessage) = _validationService.ValidateCommandLineArgs(args);

            // Assert
            Assert.True(isValid);
            Assert.Empty(errorMessage);
        }

        [Fact]
        public void ValidateCommandLineArgs_WithEmptyArgs_ShouldReturnInvalid()
        {
            // Arrange
            string[] args = { };

            // Act
            var (isValid, errorMessage) = _validationService.ValidateCommandLineArgs(args);

            // Assert
            Assert.False(isValid);
            Assert.Equal("Please provide the input file path as a command line argument.", errorMessage);
        }

        [Fact]
        public void ValidateCommandLineArgs_WithEmptyStringArg_ShouldReturnValid()
        {
            // Arrange
            string[] args = { "" };

            // Act
            var (isValid, errorMessage) = _validationService.ValidateCommandLineArgs(args);

            // Assert
            Assert.True(isValid);
            Assert.Empty(errorMessage);
        }

        [Fact]
        public void ValidateCommandLineArgs_WithWhitespaceArg_ShouldReturnValid()
        {
            // Arrange
            string[] args = { "   " };

            // Act
            var (isValid, errorMessage) = _validationService.ValidateCommandLineArgs(args);

            // Assert
            Assert.True(isValid);
            Assert.Empty(errorMessage);
        }
    }
} 