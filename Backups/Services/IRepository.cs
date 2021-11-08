using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public interface IRepository
    {
        Repository AddRepository(string name);
        string SingleStorage(List<string> paths);
        void DeleteStorage(string path);
        void AddZipArchive(string path, string destination);
    }
}