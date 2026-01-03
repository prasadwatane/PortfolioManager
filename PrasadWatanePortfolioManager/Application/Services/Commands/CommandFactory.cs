using PrasadWatanePortfolioManager.Application.Services;

namespace PrasadWatanePortfolioManager.Application.Services.Commands
{
    public class CommandFactory
    {
        private readonly PortfolioManager _portfolioManager;

        public CommandFactory(PortfolioManager portfolioManager)
        {
            _portfolioManager = portfolioManager;
        }

        public ICommand CreateCommand(string commandName)
        {
            return commandName switch
            {
                "CURRENT_PORTFOLIO" => new CurrentPortfolioCommand(_portfolioManager),
                "CALCULATE_OVERLAP" => new CalculateOverlapCommand(_portfolioManager),
                "ADD_STOCK" => new AddStockCommand(_portfolioManager),
                _ => new UnknownCommand()
            };
        }
    }

    public class UnknownCommand : ICommand
    {
        public void Execute(string[] args)
        {
            Console.WriteLine($"Unknown command: {args.FirstOrDefault() ?? "N/A"}");
        }
    }
}