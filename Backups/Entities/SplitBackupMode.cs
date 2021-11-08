using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Entities
{
    public class SplitBackupMode : IBackupMode
    {
        public RestorePoint RunJobObjectWithoutArchiving(IRepository repository, List<JobObject> jobObjects, int restorePointsCount)
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), repository);
            foreach (var jobObject in jobObjects)
            {
                string newStorageName = jobObject.Name + "_" + (restorePointsCount + 1).ToString();
                Storage newStorage = restorePoint.AddStorage(newStorageName, jobObject.Paths);
            }

            return restorePoint;
        }

        public RestorePoint RunJobObject(IRepository repository, List<JobObject> jobObjects, int restorePointsCount)
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now.ToString(), repository);
            string repositoryName = @"\RestorePoint" + (restorePointsCount + 1).ToString();
            Repository rpRepository = repository.AddRepository(repositoryName);
            foreach (var jobObject in jobObjects)
            {
                string jobRepositoryName = repositoryName.Substring(repositoryName.LastIndexOf(@"\")) + @"\" + jobObject.Name;
                Repository jobRepository = repository.AddRepository(jobRepositoryName);
                string newStorageName = jobObject.Name + "_" + (restorePointsCount + 1).ToString();
                Storage newStorage = restorePoint.AddStorage(newStorageName, jobObject.Paths);
                newStorage.Archive(jobRepository.Path, repository);
                newStorage.Paths = new List<string>() { jobRepository.Path };
            }

            return restorePoint;
        }

        public string GetMode()
        {
            return "Split";
        }
    }
}