using PrasadWatanePortfolioManager.Domain;
namespace PrasadWatanePortfolioManager.Tests.Models;

public class PortfolioTests
{
    private readonly Dictionary<string, Fund> _testFunds;

    public PortfolioTests()
    {
        _testFunds = new Dictionary<string, Fund>
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
    public void Constructor_WithNullFunds_ShouldCreateEmptyPortfolio()
    {
        var portfolio = new Portfolio(null);
        Assert.False(portfolio.HasFund("ANY_FUND"));
    }

    [Fact]
    public void SetCurrentFunds_WithValidFunds_ShouldSetCurrentFunds()
    {
        var portfolio = new Portfolio(_testFunds);
        portfolio.SetCurrentFunds(new[] { "FUND_1", "FUND_2" });

        Assert.True(portfolio.HasFund("FUND_1"));
        Assert.True(portfolio.HasFund("FUND_2"));
    }

    [Fact]
    public void SetCurrentFunds_WithInvalidFunds_ShouldIgnoreInvalidFunds()
    {
        var portfolio = new Portfolio(_testFunds);
        portfolio.SetCurrentFunds(new[] { "FUND_1", "INVALID_FUND", "FUND_2" });

        Assert.True(portfolio.HasFund("FUND_1"));
        Assert.True(portfolio.HasFund("FUND_2"));
        Assert.False(portfolio.HasFund("INVALID_FUND"));
    }

    [Fact]
    public void AddStockToFund_WithValidFund_ShouldAddStock()
    {
        var portfolio = new Portfolio(_testFunds);
        portfolio.SetCurrentFunds(new[] { "FUND_1" });

        portfolio.AddStockToFund("FUND_1", "NEW_STOCK");

        var fund = _testFunds["FUND_1"];
        Assert.Equal(4, fund.StockCount);
    }

    [Fact]
    public void CalculateOverlaps_WithPerfectOverlap_ShouldReturnCorrectPercentage()
    {
        var fund1 = CreateFund("FUND_1", "STOCK_1", "STOCK_2");
        var fund2 = CreateFund("FUND_2", "STOCK_1", "STOCK_2");
        var funds = new Dictionary<string, Fund> { ["FUND_1"] = fund1, ["FUND_2"] = fund2 };

        var portfolio = new Portfolio(funds);
        portfolio.SetCurrentFunds(new[] { "FUND_2" });

        var overlaps = portfolio.CalculateOverlaps("FUND_1").ToList();

        Assert.Single(overlaps);
        Assert.Equal(100.0, overlaps[0].OverlapPercentage, 2);
    }
}