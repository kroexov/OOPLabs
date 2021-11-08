using Backups.Entities;

namespace Backups.Services
{
    public interface IArchiver
    {
        Archiver CreateArchiver();
        void AddPath(string path, Archiver archiver);
        void MakeArchive(Archiver archiver, string name, string destination, IRepository repository);
    }
}