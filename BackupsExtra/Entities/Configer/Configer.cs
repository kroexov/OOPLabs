using System.IO;
using Backups.Services;

namespace BackupsExtra.Entities.Configer
{
    public class Configer
    {
        private string _path = Directory.GetCurrentDirectory() + @"\Config.txt";

        public string Path
        {
            get => _path;
        }

        public void AddConfig(string message)
        {
            File.WriteAllTextAsync(_path, message);
        }
    }
}