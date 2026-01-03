using System.Text.Json;
using PrasadWatanePortfolioManager.Domain;
using PrasadWatanePortfolioManager.Infra.Data.Models;

namespace PrasadWatanePortfolioManager.Infra.Data
{
    public interface IFundRepository
    {
        Dictionary<string, Fund> LoadFunds();
    }

    public class FundRepository : IFundRepository
    {
        private readonly string _fundsFilePath;

        public FundRepository(string fundsFilePath = "prasad_watane_funds.json")
        {
            _fundsFilePath = fundsFilePath;
        }

        public Dictionary<string, Fund> LoadFunds()
        {
            try
            {
                var jsonContent = File.ReadAllText(_fundsFilePath);
                var fundsData = JsonSerializer.Deserialize<FundsData>(jsonContent);

                return fundsData?.funds?.ToDictionary(
                    f => f.name,
                    f => CreateFund(f)
                ) ?? new Dictionary<string, Fund>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading funds: {ex.Message}");
                return new Dictionary<string, Fund>();
            }
        }

        private static Fund CreateFund(FundData fundData)
        {
            var fund = new Fund(fundData.name);
            foreach (var stock in fundData.stocks)
            {
                fund.AddStock(stock);
            }
            return fund;
        }
    }
} 