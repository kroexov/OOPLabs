using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class Archiver
    {
        private List<string> _paths = new List<string>();

        public void AddPath(string path)
        {
            _paths.Add(path);
        }

        public void MakeArchive(string index, string destination, IRepository repository)
        {
            foreach (var path in _paths)
            {
                string name = path.Substring(path.LastIndexOf(@"\")) + index.Substring(index.LastIndexOf("_"));
                string dest = destination + name + ".zip";
                repository.AddZipArchive(path, dest);
            }
        }
    }
}