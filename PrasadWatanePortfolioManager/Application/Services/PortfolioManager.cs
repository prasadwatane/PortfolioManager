using PrasadWatanePortfolioManager.Domain;
using PrasadWatanePortfolioManager.Infra.Data;

namespace PrasadWatanePortfolioManager.Application.Services
{
    public class PortfolioManager
    {
        private readonly Portfolio _portfolio;

        public PortfolioManager(IFundRepository fundRepository)
        {
            var funds = fundRepository.LoadFunds();
            _portfolio = new Portfolio(funds);
        }

        public void SetCurrentPortfolio(string[] fundNames)
        {
            _portfolio.SetCurrentFunds(fundNames);
        }

        public void CalculateOverlap(string fundName)
        {
            if (!_portfolio.HasFund(fundName))
            {
                Console.WriteLine("FUND_NOT_FOUND");
                return;
            }

            var overlaps = _portfolio.CalculateOverlaps(fundName);
            foreach (var overlap in overlaps)
            {
                Console.WriteLine($"{overlap.TargetFund} {overlap.PortfolioFund} {overlap.OverlapPercentage:F2}%");
            }
        }

        public void AddStock(string fundName, string stockName)
        {
            if (!_portfolio.HasFund(fundName))
            {
                Console.WriteLine("FUND_NOT_FOUND");
                return;
            }

            _portfolio.AddStockToFund(fundName, stockName);
        }
    }
}