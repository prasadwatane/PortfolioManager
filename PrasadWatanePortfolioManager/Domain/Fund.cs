namespace PrasadWatanePortfolioManager.Domain
{
    public class Fund
    {
        private readonly HashSet<string> _stocks;

        public Fund(string name)
        {
            Name = name;
            _stocks = new HashSet<string>();
        }

        public string Name { get; }

        public void AddStock(string stock)
        {
            _stocks.Add(stock);
        }

        public int StockCount => _stocks.Count;

        public bool ContainsStock(string stock) => _stocks.Contains(stock);

        public int GetCommonStocksCount(Fund other)
        {
            return _stocks.Intersect(other._stocks).Count();
        }
    }
}