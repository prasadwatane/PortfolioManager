using System.Collections.Generic;
using System.Linq;

namespace PrasadWatanePortfolioManager.Domain
{
    public class Portfolio
    {
        private readonly Dictionary<string, Fund> _funds;
        private readonly List<string> _currentFundNames;

        public Portfolio(Dictionary<string, Fund> funds)
        {
            _funds = funds ?? new Dictionary<string, Fund>();
            _currentFundNames = new List<string>();
        }

        public void SetCurrentFunds(string[] fundNames)
        {
            _currentFundNames.Clear();
            foreach (var fundName in fundNames)
            {
                if (_funds.ContainsKey(fundName))
                {
                    _currentFundNames.Add(fundName);
                }
            }
        }

        public void AddStockToFund(string fundName, string stockName)
        {
            if (_funds.TryGetValue(fundName, out var fund))
            {
                fund.AddStock(stockName);
            }
        }

        public bool HasFund(string fundName) => _funds.ContainsKey(fundName);

        public IEnumerable<OverlapResult> CalculateOverlaps(string targetFundName)
        {
            if (!_funds.TryGetValue(targetFundName, out var targetFund))
                yield break;

            foreach (var portfolioFundName in _currentFundNames)
            {
                if (_funds.TryGetValue(portfolioFundName, out var portfolioFund))
                {
                    var overlapPercentage = CalculateOverlapPercentage(targetFund, portfolioFund);
                    if (overlapPercentage > 0)
                    {
                        yield return new OverlapResult(targetFundName, portfolioFundName, overlapPercentage);
                    }
                }
            }
        }

        private static double CalculateOverlapPercentage(Fund fund1, Fund fund2)
        {
            var commonStocks = fund1.GetCommonStocksCount(fund2);
            var totalStocks = fund1.StockCount + fund2.StockCount;

            return totalStocks == 0 ? 0 : 2.0 * commonStocks / totalStocks * 100;
        }
    }
}