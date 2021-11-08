using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Entities
{
    public class SingleBackupMode : IBackupMode
    {
        private string _curDir = Directory.GetCurrentDirectory();
        public RestorePoint RunJobObjectWithoutArchiving(IRepository repository, List<JobObject> jobObjects, int restorePointsCount)
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), repository);
            List<string> paths = new List<string>();
            foreach (var jobObject in jobObjects)
            {
                foreach (var path in jobObject.Paths)
                {
                    paths.Add(path);
                }
            }

            string newStorageName = "_" + (restorePointsCount + 1).ToString();
            Storage newStorage = restorePoint.AddStorage(newStorageName, paths);
            return restorePoint;
        }

        public RestorePoint RunJobObject(IRepository repository, List<JobObject> jobObjects, int restorePointsCount)
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), repository);
            string repositoryName = @"\RestorePoint" + (restorePointsCount + 1).ToString();
            Repository rpRepository = repository.AddRepository(repositoryName);

            List<string> paths = new List<string>();
            foreach (var jobObject in jobObjects)
            {
                foreach (var path in jobObject.Paths)
                {
                    paths.Add(path);
                }
            }

            string singleStoragePath = repository.SingleStorage(paths);
            string newStorageName = "_" + (restorePointsCount + 1).ToString();
            Storage newStorage = restorePoint.AddStorage(newStorageName, paths);
            newStorage.SingleArchive(rpRepository.Path, singleStoragePath, repository);
            repository.DeleteStorage(_curDir + @"\temp");
            newStorage.Paths = new List<string>() { rpRepository.Path };
            return restorePoint;
        }

        public string GetMode()
        {
            return "Single";
        }
    }
}