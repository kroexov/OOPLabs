using System.IO;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.Loggers
{
    public class FileLogger : Logger
    {
        private string _path;

        private FileLogger(string path)
        {
            _path = path;
        }

        public override void MakeLogMessage(string message)
        {
            File.WriteAllTextAsync(_path, message);
        }
    }
}