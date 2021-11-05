using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private string _date;
        private List<Storage> _storages = new List<Storage>();
        private IIRepository _repository;

        public RestorePoint(string date, IIRepository repository)
        {
            _repository = repository;
            _date = date;
        }

        public string Date
        {
            get
            {
                return _date;
            }
        }

        public List<Storage> Storages
        {
            get
            {
                return _storages;
            }
        }

        public Storage AddStorage(string name, List<string> paths)
        {
            Storage storage = new Storage(name, paths, _repository);
            _storages.Add(storage);
            return storage;
        }
    }
}