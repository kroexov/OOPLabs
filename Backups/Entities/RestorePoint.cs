using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private DateTime _date;
        private List<Storage> _storages = new List<Storage>();
        private IRepository _repository;

        public RestorePoint(DateTime date, IRepository repository)
        {
            _repository = repository;
            _date = date;
        }

        public DateTime Date
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