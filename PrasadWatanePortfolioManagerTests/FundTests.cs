using PrasadWatanePortfolioManager.Domain;

namespace PrasadWatanePortfolioManager.Tests.Models;

public class FundTests
{
    [Fact]
    public void Constructor_ShouldSetName()
    {
        // Arrange & Act
        var fund = new Fund("TEST_FUND");

        // Assert
        Assert.Equal("TEST_FUND", fund.Name);
    }

    [Fact]
    public void AddStock_ShouldAddStockToFund()
    {
        // Arrange
        var fund = new Fund("TEST_FUND");

        // Act
        fund.AddStock("HDFC BANK LIMITED");

        // Assert
        Assert.Equal(1, fund.StockCount);
    }

    [Fact]
    public void AddStock_DuplicateStock_ShouldNotAddAgain()
    {
        // Arrange
        var fund = new Fund("TEST_FUND");
        fund.AddStock("HDFC BANK LIMITED");

        // Act
        fund.AddStock("HDFC BANK LIMITED");

        // Assert
        Assert.Equal(1, fund.StockCount);
    }

    [Fact]
    public void AddStock_MultipleStocks_ShouldAddAll()
    {
        // Arrange
        var fund = new Fund("TEST_FUND");

        // Act
        fund.AddStock("HDFC BANK LIMITED");
        fund.AddStock("INFOSYS LIMITED");
        fund.AddStock("ICICI BANK LIMITED");

        // Assert
        Assert.Equal(3, fund.StockCount);
    }

    [Fact]
    public void GetCommonStocksCount_NoCommonStocks_ShouldReturnZero()
    {
        // Arrange
        var fund1 = new Fund("FUND_1");
        fund1.AddStock("HDFC BANK LIMITED");
        fund1.AddStock("INFOSYS LIMITED");

        var fund2 = new Fund("FUND_2");
        fund2.AddStock("TCS");
        fund2.AddStock("WIPRO LIMITED");

        // Act
        var commonCount = fund1.GetCommonStocksCount(fund2);

        // Assert
        Assert.Equal(0, commonCount);
    }

    [Fact]
    public void GetCommonStocksCount_WithCommonStocks_ShouldReturnCorrectCount()
    {
        // Arrange
        var fund1 = new Fund("FUND_1");
        fund1.AddStock("HDFC BANK LIMITED");
        fund1.AddStock("INFOSYS LIMITED");
        fund1.AddStock("ICICI BANK LIMITED");

        var fund2 = new Fund("FUND_2");
        fund2.AddStock("HDFC BANK LIMITED");
        fund2.AddStock("TCS");
        fund2.AddStock("INFOSYS LIMITED");

        // Act
        var commonCount = fund1.GetCommonStocksCount(fund2);

        // Assert
        Assert.Equal(2, commonCount);
    }

    [Fact]
    public void GetCommonStocksCount_AllStocksCommon_ShouldReturnFundWithLessStocks()
    {
        // Arrange
        var fund1 = new Fund("FUND_1");
        fund1.AddStock("HDFC BANK LIMITED");
        fund1.AddStock("INFOSYS LIMITED");

        var fund2 = new Fund("FUND_2");
        fund2.AddStock("HDFC BANK LIMITED");
        fund2.AddStock("INFOSYS LIMITED");
        fund2.AddStock("TCS");

        // Act
        var commonCount = fund1.GetCommonStocksCount(fund2);

        // Assert
        Assert.Equal(2, commonCount);
    }

    [Fact]
    public void StockCount_EmptyFund_ShouldReturnZero()
    {
        // Arrange & Act
        var fund = new Fund("EMPTY_FUND");

        // Assert
        Assert.Equal(0, fund.StockCount);
    }

    [Fact]
    public void StockCount_AfterAddingStocks_ShouldReturnCorrectCount()
    {
        // Arrange
        var fund = new Fund("TEST_FUND");

        // Act
        fund.AddStock("STOCK_1");
        fund.AddStock("STOCK_2");
        fund.AddStock("STOCK_3");

        // Assert
        Assert.Equal(3, fund.StockCount);
    }
}