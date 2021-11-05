using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public class IArchiver : IIArchiver
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

        public void MakeArchive(Archiver archiver, string name, string destination)
        {
            archiver.MakeArchive(name, destination);
        }
    }
}