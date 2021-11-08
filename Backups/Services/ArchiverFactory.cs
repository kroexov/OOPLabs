using Backups.Entities;

namespace Backups.Services
{
    public class ArchiverFactory : IArchiver
    {
        public Archiver CreateArchiver()
        {
            Archiver archiver = new Archiver();
            return archiver;
        }

        public void AddPath(string path, Archiver archiver)
        {
            archiver.AddPath(path);
        }

        public void MakeArchive(Archiver archiver, string name, string destination, IRepository repository)
        {
            archiver.MakeArchive(name, destination, repository);
        }
    }
}