using System.Collections.Generic;

namespace Backups.Entities
{
    public class Repository
    {
        private string _directory;

        public Repository(string path)
        {
            _directory = path;
        }

        public string Path
        {
            get
            {
                return _directory;
            }
        }
    }
}