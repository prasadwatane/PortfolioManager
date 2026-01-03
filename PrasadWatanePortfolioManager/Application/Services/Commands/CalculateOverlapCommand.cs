using PrasadWatanePortfolioManager.Application.Services;

namespace PrasadWatanePortfolioManager.Application.Services.Commands
{
    public class CalculateOverlapCommand : ICommand
    {
        private readonly PortfolioManager _portfolioManager;

        public CalculateOverlapCommand(PortfolioManager portfolioManager)
        {
            _portfolioManager = portfolioManager;
        }

        public void Execute(string[] args)
        {
            if (args.Length > 0)
            {
                _portfolioManager.CalculateOverlap(args[0]);
            }
        }
    }
}