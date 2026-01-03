namespace PrasadWatanePortfolioManager.Infra.Data.Models
{
    public class FundData
    {
        public string name { get; set; } = string.Empty;
        public List<string> stocks { get; set; } = new List<string>();
    }

    public class FundsData
    {
        public List<FundData> funds { get; set; } = new List<FundData>();
    }
} 