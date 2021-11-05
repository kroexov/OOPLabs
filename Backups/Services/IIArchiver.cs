using Backups.Entities;

namespace Backups.Services
{
    public interface IIArchiver
    {
        Archiver CreateArchiver();
        void AddPath(string path, Archiver archiver);
        void MakeArchive(Archiver archiver, string name, string destination);
    }
}