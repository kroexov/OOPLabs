using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class JobObject
    {
        private List<string> _paths = new List<string>();
        private string _name;

        public JobObject(string name)
        {
            _name = name;
        }

        public JobObject(string name, List<string> paths)
        {
            _name = name;
            _paths = paths;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public List<string> Paths
        {
            get
            {
                return _paths;
            }
        }

        public void AddPath(string path)
        {
            _paths.Add(path);
        }

        public void DeletePath(string path)
        {
            _paths.Remove(path);
        }
    }
}