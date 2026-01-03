using PrasadWatanePortfolioManager.Application.Services;
using PrasadWatanePortfolioManager.Application.Services.Commands;
using PrasadWatanePortfolioManager.Infra.FileSystem;
using Moq;
using Xunit;

namespace PrasadWatanePortfolioManager.Tests.Application.Services
{
    public class ApplicationServiceTests
    {
        private readonly Mock<IFileReader> _mockFileReader;

        private readonly Mock<PortfolioManager> _mockPortfolioManager;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTests()
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockPortfolioManager = new Mock<PortfolioManager>(Mock.Of<PrasadWatanePortfolioManager.Infra.Data.IFundRepository>());
            _applicationService = new ApplicationService(_mockPortfolioManager.Object, _mockFileReader.Object);
        }

        [Fact]
        public void ProcessInputFile_FileDoesNotExist_ShouldPrintErrorMessage()
        {
            // Arrange
            string inputFilePath = "nonexistent.txt";
            _mockFileReader.Setup(x => x.FileExists(inputFilePath)).Returns(false);

            // Act
            _applicationService.ProcessInputFile(inputFilePath);

            // Assert
            _mockFileReader.Verify(x => x.FileExists(inputFilePath), Times.Once);
        }

        [Fact]
        public void ProcessInputFile_ValidFile_ShouldProcessAllLines()
        {
            // Arrange
            string inputFilePath = "test.txt";
            string[] lines = {
                "CURRENT_PORTFOLIO FUND1 FUND2",
                "CALCULATE_OVERLAP FUND1",
                "ADD_STOCK FUND1 STOCK1"
            };

            _mockFileReader.Setup(x => x.FileExists(inputFilePath)).Returns(true);
            _mockFileReader.Setup(x => x.ReadAllLines(inputFilePath)).Returns(lines);

            // Act
            _applicationService.ProcessInputFile(inputFilePath);

            // Assert
            _mockFileReader.Verify(x => x.FileExists(inputFilePath), Times.Once);
            _mockFileReader.Verify(x => x.ReadAllLines(inputFilePath), Times.Once);
        }

        [Fact]
        public void ProcessInputFile_EmptyLines_ShouldSkipEmptyLines()
        {
            // Arrange
            string inputFilePath = "test.txt";
            string[] lines = {
                "CURRENT_PORTFOLIO FUND1",
                "",
                "   ",
                "CALCULATE_OVERLAP FUND1"
            };

            _mockFileReader.Setup(x => x.FileExists(inputFilePath)).Returns(true);
            _mockFileReader.Setup(x => x.ReadAllLines(inputFilePath)).Returns(lines);

            // Act
            _applicationService.ProcessInputFile(inputFilePath);

            // Assert
            _mockFileReader.Verify(x => x.ReadAllLines(inputFilePath), Times.Once);
        }

        [Fact]
        public void ProcessInputFile_UnknownCommand_ShouldCreateUnknownCommand()
        {
            // Arrange
            string inputFilePath = "test.txt";
            string[] lines = { "UNKNOWN_COMMAND ARG1 ARG2" };

            _mockFileReader.Setup(x => x.FileExists(inputFilePath)).Returns(true);
            _mockFileReader.Setup(x => x.ReadAllLines(inputFilePath)).Returns(lines);

            // Act
            _applicationService.ProcessInputFile(inputFilePath);

            // Assert
            _mockFileReader.Verify(x => x.ReadAllLines(inputFilePath), Times.Once);
        }
    }
} 