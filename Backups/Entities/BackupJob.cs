using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Entities
{
    public class BackupJob
    {
        private string _curDir = Directory.GetCurrentDirectory();
        private string _name;
        private string _mode = "split";
        private List<JobObject> _jobObjects = new List<JobObject>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private IIRepository _repository;

        public BackupJob(string name, IIRepository repository)
        {
            _repository = repository;
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Mode
        {
            get
            {
                return _mode;
            }
        }

        public List<JobObject> JobObjects
        {
            get
            {
                return _jobObjects;
            }
        }

        public List<RestorePoint> RestorePoints
        {
            get
            {
                return _restorePoints;
            }
        }

        public void ChangeMode()
        {
            if (_mode.Equals("split"))
            {
                _mode = "single";
                Console.WriteLine("changed mode to single");
            }
            else
            {
                _mode = "split";
                Console.WriteLine("changed mode to split");
            }
        }

        public RestorePoint RunJobObjectsWithoutDirectories()
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), _repository);
            if (_mode == "split")
            {
                foreach (var jobObject in _jobObjects)
                {
                    string newStorageName = jobObject.Name + "_" + (_restorePoints.Count + 1).ToString();
                    Storage newStorage = restorePoint.AddStorage(newStorageName, jobObject.Paths);
                }
            }

            if (_mode == "single")
            {
                List<string> paths = new List<string>();
                foreach (var jobObject in _jobObjects)
                {
                    foreach (var path in jobObject.Paths)
                    {
                        paths.Add(path);
                    }
                }

                string newStorageName = _name + "_" + (_restorePoints.Count + 1).ToString();
                Storage newStorage = restorePoint.AddStorage(newStorageName, paths);
            }

            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public RestorePoint RunJobObjects()
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), _repository);
            string repositoryName = @"\RestorePoint" + (_restorePoints.Count + 1).ToString();
            Repository rpRepository = _repository.AddRepository(repositoryName);
            if (_mode == "split")
            {
                foreach (var jobObject in _jobObjects)
                {
                    string jobRepositoryName = repositoryName.Substring(repositoryName.LastIndexOf(@"\")) + @"\" + jobObject.Name;
                    Repository jobRepository = _repository.AddRepository(jobRepositoryName);
                    string newStorageName = jobObject.Name + "_" + (_restorePoints.Count + 1).ToString();
                    Storage newStorage = restorePoint.AddStorage(newStorageName, jobObject.Paths);
                    newStorage.Archive(jobRepository.Path);
                    newStorage.Paths = new List<string>() { jobRepository.Path };
                }
            }

            if (_mode == "single")
            {
                List<string> paths = new List<string>();
                foreach (var jobObject in _jobObjects)
                {
                    foreach (var path in jobObject.Paths)
                    {
                        paths.Add(path);
                    }
                }

                string singleStoragePath = _repository.SingleStorage(paths);
                string newStorageName = _name + "_" + (_restorePoints.Count + 1).ToString();
                Storage newStorage = restorePoint.AddStorage(newStorageName, paths);
                newStorage.SingleArchive(rpRepository.Path, singleStoragePath);
                _repository.DeleteStorage(_curDir + @"\temp");
                newStorage.Paths = new List<string>() { rpRepository.Path };
            }

            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
            _restorePoints.Add(restorePoint);
        }

        public void RemoveJobObject(string name)
        {
            foreach (var jobObject in _jobObjects)
            {
                if (jobObject.Name.Equals(name))
                {
                    _jobObjects.Remove(jobObject);
                    return;
                }
            }
        }
    }
}