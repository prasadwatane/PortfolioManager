namespace PrasadWatanePortfolioManager.Infra.FileSystem
{
    public interface IFileReader
    {
        bool FileExists(string filePath);
        string[] ReadAllLines(string filePath);
    }

    public class FileReader : IFileReader
    {
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
} 