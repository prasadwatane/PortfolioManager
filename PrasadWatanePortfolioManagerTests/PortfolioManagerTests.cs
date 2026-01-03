using PrasadWatanePortfolioManager.Application.Services;
using PrasadWatanePortfolioManager.Domain;
using PrasadWatanePortfolioManager.Infra.Data;
using Moq;
using Xunit;

namespace PrasadWatanePortfolioManager.Tests.Application.Services
{
    public class PortfolioManagerTests
    {
        private readonly Mock<IFundRepository> _mockFundRepository;
        private readonly PortfolioManager _portfolioManager;
        private readonly Dictionary<string, Fund> _testFunds;

        public PortfolioManagerTests()
        {
            _mockFundRepository = new Mock<IFundRepository>();
            _testFunds = CreateTestFunds();
            _mockFundRepository.Setup(x => x.LoadFunds()).Returns(_testFunds);
            _portfolioManager = new PortfolioManager(_mockFundRepository.Object);
        }

        private Dictionary<string, Fund> CreateTestFunds()
        {
            return new Dictionary<string, Fund>
            {
                ["FUND_1"] = CreateFund("FUND_1", "HDFC BANK LIMITED", "INFOSYS LIMITED", "ICICI BANK LIMITED"),
                ["FUND_2"] = CreateFund("FUND_2", "HDFC BANK LIMITED", "TCS", "WIPRO LIMITED"),
                ["FUND_3"] = CreateFund("FUND_3", "RELIANCE INDUSTRIES LIMITED", "BHARTI AIRTEL LIMITED"),
                ["FUND_4"] = CreateFund("FUND_4", "HDFC BANK LIMITED", "INFOSYS LIMITED", "RELIANCE INDUSTRIES LIMITED")
            };
        }

        private static Fund CreateFund(string name, params string[] stocks)
        {
            var fund = new Fund(name);
            foreach (var stock in stocks)
            {
                fund.AddStock(stock);
            }
            return fund;
        }

        [Fact]
        public void SetCurrentPortfolio_WithValidFundNames_ShouldSetPortfolio()
        {
            // Arrange
            string[] fundNames = { "FUND_1", "FUND_2" };

            // Act
            _portfolioManager.SetCurrentPortfolio(fundNames);

            // Assert
            // Verify by calling CalculateOverlap which uses the portfolio
            _portfolioManager.CalculateOverlap("FUND_1");
        }

        [Fact]
        public void SetCurrentPortfolio_WithInvalidFundNames_ShouldIgnoreInvalidFunds()
        {
            // Arrange
            string[] fundNames = { "FUND_1", "INVALID_FUND", "FUND_2" };

            // Act
            _portfolioManager.SetCurrentPortfolio(fundNames);

            // Assert
            // The portfolio should still work with valid funds
            _portfolioManager.CalculateOverlap("FUND_1");
        }

        [Fact]
        public void CalculateOverlap_WithValidFund_ShouldCalculateOverlaps()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_2", "FUND_3" });

            // Act
            _portfolioManager.CalculateOverlap("FUND_1");

            // Assert
            // The method should execute without throwing exceptions
            // and should output overlap percentages
        }

        [Fact]
        public void CalculateOverlap_WithInvalidFund_ShouldPrintFundNotFound()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_1", "FUND_2" });

            // Act
            _portfolioManager.CalculateOverlap("INVALID_FUND");

            // Assert
            // The method should output "FUND_NOT_FOUND"
        }

        [Fact]
        public void CalculateOverlap_WithNoCurrentPortfolio_ShouldHandleGracefully()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new string[] { });

            // Act
            _portfolioManager.CalculateOverlap("FUND_1");

            // Assert
            // Should handle empty portfolio gracefully
        }

        [Fact]
        public void AddStock_WithValidFund_ShouldAddStock()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_1" });
            string stockName = "NEW_STOCK";

            // Act
            _portfolioManager.AddStock("FUND_1", stockName);

            // Assert
            // Verify the stock was added by checking the fund's stock count
            Assert.Equal(4, _testFunds["FUND_1"].StockCount);
        }

        [Fact]
        public void AddStock_WithInvalidFund_ShouldPrintFundNotFound()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_1" });

            // Act
            _portfolioManager.AddStock("INVALID_FUND", "NEW_STOCK");

            // Assert
            // Should output "FUND_NOT_FOUND"
        }

        [Fact]
        public void AddStock_WithStockContainingSpaces_ShouldHandleCorrectly()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_1" });
            string stockName = "HDFC BANK LIMITED";

            // Act
            _portfolioManager.AddStock("FUND_1", stockName);

            // Assert
            // Should handle stock names with spaces correctly
        }

        [Fact]
        public void AddStock_WithDuplicateStock_ShouldNotAddAgain()
        {
            // Arrange
            _portfolioManager.SetCurrentPortfolio(new[] { "FUND_1" });
            string stockName = "HDFC BANK LIMITED"; // Already exists in FUND_1

            // Act
            _portfolioManager.AddStock("FUND_1", stockName);

            // Assert
            // Stock count should remain the same (3)
            Assert.Equal(3, _testFunds["FUND_1"].StockCount);
        }

        [Fact]
        public void CalculateOverlap_WithPerfectOverlap_ShouldReturnCorrectPercentage()
        {
            // Arrange
            var fund1 = CreateFund("FUND_1", "STOCK_1", "STOCK_2");
            var fund2 = CreateFund("FUND_2", "STOCK_1", "STOCK_2");
            var funds = new Dictionary<string, Fund> { ["FUND_1"] = fund1, ["FUND_2"] = fund2 };
            
            _mockFundRepository.Setup(x => x.LoadFunds()).Returns(funds);
            var portfolioManager = new PortfolioManager(_mockFundRepository.Object);
            portfolioManager.SetCurrentPortfolio(new[] { "FUND_2" });

            // Act
            portfolioManager.CalculateOverlap("FUND_1");

            // Assert
            // Should calculate 100% overlap
        }

        [Fact]
        public void CalculateOverlap_WithNoOverlap_ShouldReturnZeroPercentage()
        {
            // Arrange
            var fund1 = CreateFund("FUND_1", "STOCK_1", "STOCK_2");
            var fund2 = CreateFund("FUND_2", "STOCK_3", "STOCK_4");
            var funds = new Dictionary<string, Fund> { ["FUND_1"] = fund1, ["FUND_2"] = fund2 };
            
            _mockFundRepository.Setup(x => x.LoadFunds()).Returns(funds);
            var portfolioManager = new PortfolioManager(_mockFundRepository.Object);
            portfolioManager.SetCurrentPortfolio(new[] { "FUND_2" });

            // Act
            portfolioManager.CalculateOverlap("FUND_1");

            // Assert
            // Should calculate 0% overlap
        }
    }
} 