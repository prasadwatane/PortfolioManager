using PrasadWatanePortfolioManager.Application.Services;
using PrasadWatanePortfolioManager.Application.Services.Commands;
using Moq;
using Xunit;

namespace PrasadWatanePortfolioManager.Tests.Application.Services.Commands
{
    public class CommandFactoryTests
    {
        private readonly Mock<PortfolioManager> _mockPortfolioManager;
        private readonly CommandFactory _commandFactory;

        public CommandFactoryTests()
        {
            _mockPortfolioManager = new Mock<PortfolioManager>(Mock.Of<PrasadWatanePortfolioManager.Infra.Data.IFundRepository>());
            _commandFactory = new CommandFactory(_mockPortfolioManager.Object);
        }

        [Fact]
        public void CreateCommand_WithCurrentPortfolioCommand_ShouldReturnCurrentPortfolioCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("CURRENT_PORTFOLIO");

            // Assert
            Assert.IsType<CurrentPortfolioCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithCalculateOverlapCommand_ShouldReturnCalculateOverlapCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("CALCULATE_OVERLAP");

            // Assert
            Assert.IsType<CalculateOverlapCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithAddStockCommand_ShouldReturnAddStockCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("ADD_STOCK");

            // Assert
            Assert.IsType<AddStockCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithUnknownCommand_ShouldReturnUnknownCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("UNKNOWN_COMMAND");

            // Assert
            Assert.IsType<UnknownCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithEmptyCommand_ShouldReturnUnknownCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("");

            // Assert
            Assert.IsType<UnknownCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithNullCommand_ShouldReturnUnknownCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand(null);

            // Assert
            Assert.IsType<UnknownCommand>(command);
        }

        [Fact]
        public void CreateCommand_WithCaseInsensitiveCommand_ShouldReturnCorrectCommand()
        {
            // Act
            var command = _commandFactory.CreateCommand("current_portfolio");

            // Assert
            Assert.IsType<UnknownCommand>(command); // Should be case sensitive
        }

        [Theory]
        [InlineData("CURRENT_PORTFOLIO", typeof(CurrentPortfolioCommand))]
        [InlineData("CALCULATE_OVERLAP", typeof(CalculateOverlapCommand))]
        [InlineData("ADD_STOCK", typeof(AddStockCommand))]
        [InlineData("UNKNOWN", typeof(UnknownCommand))]
        [InlineData("", typeof(UnknownCommand))]
        public void CreateCommand_WithVariousCommands_ShouldReturnCorrectCommandType(string commandName, Type expectedType)
        {
            // Act
            var command = _commandFactory.CreateCommand(commandName);

            // Assert
            Assert.IsType(expectedType, command);
        }
    }
} 