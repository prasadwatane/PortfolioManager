using PrasadWatanePortfolioManager.Application.Services.Commands;
using PrasadWatanePortfolioManager.Infra.FileSystem;

namespace PrasadWatanePortfolioManager.Application.Services
{
    public interface IApplicationService
    {
        void ProcessInputFile(string inputFilePath);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly PortfolioManager _portfolioManager;
        private readonly CommandFactory _commandFactory;
        private readonly IFileReader _fileReader;

        public ApplicationService(PortfolioManager portfolioManager, IFileReader fileReader)
        {
            _portfolioManager = portfolioManager;
            _commandFactory = new CommandFactory(portfolioManager);
            _fileReader = fileReader;
        }

        public void ProcessInputFile(string inputFilePath)
        {
            if (!_fileReader.FileExists(inputFilePath))
            {
                Console.WriteLine($"Input file not found: {inputFilePath}");
                return;
            }

            try
            {
                string[] lines = _fileReader.ReadAllLines(inputFilePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    string commandName = parts[0];
                    string[] commandArgs = parts.Skip(1).ToArray();

                    var command = _commandFactory.CreateCommand(commandName);
                    command.Execute(commandArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }
    }
} 