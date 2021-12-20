using System.IO;
using Backups.Services;

namespace BackupsExtra.Entities.Configer
{
    public class Configer
    {
        private string _path =
            @"C:\Users\Citilink\Desktop\лабы решотка 1 сем\kroexov\BackupsExtra\Entities\Configer\Config.txt";

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