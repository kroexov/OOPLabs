using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public class PointRestorer
    {
        private string _curDir = Directory.GetCurrentDirectory();
        public void RestoreThePoint(RestorePoint restorePoint)
        {
            foreach (var storage in restorePoint.Storages)
            {
                ZipFile.ExtractToDirectory(storage.Paths[0], _curDir + @"\BackupRoot");
            }
        }

        public void RestoreThePoint(RestorePoint restorePoint, string path)
        {
            foreach (var storage in restorePoint.Storages)
            {
                ZipFile.ExtractToDirectory(storage.Paths[0], path);
            }
        }
    }
}