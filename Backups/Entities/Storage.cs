using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class Storage
    {
        private string _name;
        private List<string> _paths = new List<string>();
        private IIArchiver _archiver = new IArchiver();
        private IIRepository _repository;

        public Storage(string name, List<string> paths, IIRepository repository)
        {
            _paths = paths;
            _repository = repository;
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public List<string> Paths
        {
            get
            {
                return _paths;
            }
            set
            {
                _paths = value;
            }
        }

        public void Archive(string destination)
        {
            Archiver archiver = _archiver.CreateArchiver();
            foreach (var path in _paths)
            {
                _archiver.AddPath(path, archiver);
            }

            _archiver.MakeArchive(archiver, _name, destination);
        }

        public void SingleArchive(string destination, string singlepath)
        {
            Archiver archiver = _archiver.CreateArchiver();
            _archiver.AddPath(singlepath, archiver);
            _archiver.MakeArchive(archiver, _name, destination);
        }
    }
}