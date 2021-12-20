using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Entities
{
    public class BackupJob
    {
        private string _name;
        private IBackupMode _backupMode;
        private List<JobObject> _jobObjects = new List<JobObject>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private IRepository _repository;

        public BackupJob(string name, IRepository repository)
        {
            _backupMode = new SplitBackupMode();
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
            set
            {
                _restorePoints = value;
            }
        }

        public RestorePoint RunJobObjectsWithoutDirectories()
        {
            RestorePoint restorePoint = _backupMode.RunJobObjectWithoutArchiving(_repository, _jobObjects, _restorePoints.Count);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public RestorePoint RunJobObjects()
        {
            RestorePoint restorePoint = _backupMode.RunJobObject(_repository, _jobObjects, _restorePoints.Count);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void ChangeMode()
        {
            if (_backupMode.GetMode().Equals("Single"))
            {
                _backupMode = new SplitBackupMode();
                Console.WriteLine("Changed mode to split");
            }
            else
            {
                _backupMode = new SingleBackupMode();
                Console.WriteLine("Changed mode to single");
            }
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