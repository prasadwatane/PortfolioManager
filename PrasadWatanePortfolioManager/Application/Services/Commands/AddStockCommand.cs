namespace PrasadWatanePortfolioManager.Application.Services.Commands
{
    public class AddStockCommand : ICommand
    {
        private readonly PortfolioManager _portfolioManager;

        public AddStockCommand(PortfolioManager portfolioManager)
        {
            _portfolioManager = portfolioManager;
        }

        public void Execute(string[] args)
        {
            if (args.Length > 1)
            {
                var fundName = args[0];
                var stockName = string.Join(" ", args.Skip(1));
                _portfolioManager.AddStock(fundName, stockName);
            }
        }
    }
}