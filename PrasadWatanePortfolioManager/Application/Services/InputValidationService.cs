namespace PrasadWatanePortfolioManager.Application.Services
{
    public interface IInputValidationService
    {
        (bool isValid, string errorMessage) ValidateCommandLineArgs(string[] args);
    }

    public class InputValidationService : IInputValidationService
    {
        public (bool isValid, string errorMessage) ValidateCommandLineArgs(string[] args)
        {
            if (args.Length == 0)
            {
                return (false, "Please provide the input file path as a command line argument.");
            }

            return (true, string.Empty);
        }
    }
} 