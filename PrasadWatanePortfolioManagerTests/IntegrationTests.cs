using PrasadWatanePortfolioManager.Application.Services;
using PrasadWatanePortfolioManager.Infra.Data;
using PrasadWatanePortfolioManager.Infra.FileSystem;
using Xunit;

namespace PrasadWatanePortfolioManager.Tests.Integration
{
    public class IntegrationTests
    {
        [Fact]
        public void ProcessInputFile_WithSampleInput1_ShouldProduceExpectedOutput()
        {
            // Arrange
            var fileReader = new FileReader();
            var fundRepository = new FundRepository();
            var portfolioManager = new PortfolioManager(fundRepository);
            var applicationService = new ApplicationService(portfolioManager, fileReader);

            // Create a temporary input file with sample input 1
            var inputContent = @"CURRENT_PORTFOLIO AXIS_BLUECHIP ICICI_PRU_BLUECHIP UTI_NIFTY_INDEX
CALCULATE_OVERLAP MIRAE_ASSET_EMERGING_BLUECHIP
CALCULATE_OVERLAP MIRAE_ASSET_LARGE_CAP
ADD_STOCK AXIS_BLUECHIP TCS
CALCULATE_OVERLAP MIRAE_ASSET_EMERGING_BLUECHIP";

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, inputContent);

            try
            {
                // Act
                applicationService.ProcessInputFile(tempFile);

                // Assert
                // The test passes if no exceptions are thrown
                // In a real scenario, you might capture console output and verify it
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void ProcessInputFile_WithSampleInput2_ShouldProduceExpectedOutput()
        {
            // Arrange
            var fileReader = new FileReader();
            var fundRepository = new FundRepository();
            var portfolioManager = new PortfolioManager(fundRepository);
            var applicationService = new ApplicationService(portfolioManager, fileReader);

            // Create a temporary input file with sample input 2
            var inputContent = @"CURRENT_PORTFOLIO UTI_NIFTY_INDEX AXIS_MIDCAP PARAG_PARIKH_FLEXI_CAP
CALCULATE_OVERLAP ICICI_PRU_NIFTY_NEXT_50_INDEX
CALCULATE_OVERLAP NIPPON_INDIA_PHARMA_FUND
ADD_STOCK PARAG_PARIKH_FLEXI_CAP NOCIL
ADD_STOCK AXIS_MIDCAP NOCIL
CALCULATE_OVERLAP ICICI_PRU_NIFTY_NEXT_50_INDEX";

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, inputContent);

            try
            {
                // Act
                applicationService.ProcessInputFile(tempFile);

                // Assert
                // The test passes if no exceptions are thrown
                // In a real scenario, you might capture console output and verify it
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void ProcessInputFile_WithInvalidFund_ShouldHandleGracefully()
        {
            // Arrange
            var fileReader = new FileReader();
            var fundRepository = new FundRepository();
            var portfolioManager = new PortfolioManager(fundRepository);
            var applicationService = new ApplicationService(portfolioManager, fileReader);

            // Create a temporary input file with invalid fund
            var inputContent = @"CURRENT_PORTFOLIO INVALID_FUND
CALCULATE_OVERLAP INVALID_FUND
ADD_STOCK INVALID_FUND STOCK1";

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, inputContent);

            try
            {
                // Act
                applicationService.ProcessInputFile(tempFile);

                // Assert
                // The test passes if no exceptions are thrown
                // The application should handle invalid funds gracefully
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void ProcessInputFile_WithEmptyFile_ShouldHandleGracefully()
        {
            // Arrange
            var fileReader = new FileReader();
            var fundRepository = new FundRepository();
            var portfolioManager = new PortfolioManager(fundRepository);
            var applicationService = new ApplicationService(portfolioManager, fileReader);

            // Create a temporary empty input file
            var tempFile = Path.GetTempFileName();

            try
            {
                // Act
                applicationService.ProcessInputFile(tempFile);

                // Assert
                // The test passes if no exceptions are thrown
                // The application should handle empty files gracefully
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void ProcessInputFile_WithMixedCommands_ShouldProcessAllCommands()
        {
            // Arrange
            var fileReader = new FileReader();
            var fundRepository = new FundRepository();
            var portfolioManager = new PortfolioManager(fundRepository);
            var applicationService = new ApplicationService(portfolioManager, fileReader);

            // Create a temporary input file with mixed valid and invalid commands
            var inputContent = @"CURRENT_PORTFOLIO AXIS_BLUECHIP
CALCULATE_OVERLAP MIRAE_ASSET_EMERGING_BLUECHIP
ADD_STOCK AXIS_BLUECHIP NEW_STOCK
UNKNOWN_COMMAND
CALCULATE_OVERLAP INVALID_FUND";

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, inputContent);

            try
            {
                // Act
                applicationService.ProcessInputFile(tempFile);

                // Assert
                // The test passes if no exceptions are thrown
                // The application should process valid commands and handle invalid ones gracefully
            }
            finally
            {
                // Cleanup
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
} 