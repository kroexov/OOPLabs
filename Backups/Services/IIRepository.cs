using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public interface IIRepository
    {
        Repository AddRepository(string name);
        string SingleStorage(List<string> paths);
        void DeleteStorage(string path);
    }
}