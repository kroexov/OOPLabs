using System.Collections.Generic;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupService : IBackup
    {
        private List<BackupJob> _backupJobs = new List<BackupJob>();
        private List<JobObject> _jobObjects = new List<JobObject>();
        public BackupJob CreateBackupJob(string name, IRepository repository)
        {
            BackupJob backupJob = new BackupJob(name, repository);
            _backupJobs.Add(backupJob);
            return backupJob;
        }

        public BackupJob GetBackupJob(string name)
        {
            foreach (var backupJob in _backupJobs)
            {
                if (backupJob.Name.Equals(name))
                {
                    return backupJob;
                }
            }

            throw new BackupsException("There's no backupJob with this name!");
        }

        public Backup CreateBackup(string name)
        {
            Backup backup = new Backup(name);
            return backup;
        }

        public JobObject CreateJobObject(string name, BackupJob backupJob)
        {
            JobObject jobObject = new JobObject(name);
            backupJob.AddJobObject(jobObject);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public JobObject CreateJobObject(string name, List<string> paths, BackupJob backupJob)
        {
            JobObject jobObject = new JobObject(name, paths);
            backupJob.AddJobObject(jobObject);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public RestorePoint RunBackupJob(BackupJob backupJob)
        {
            RestorePoint restorePoint = backupJob.RunJobObjects();
            return restorePoint;
        }

        public RestorePoint RunBackupJobWithoutArchiving(BackupJob backupJob)
        {
            RestorePoint restorePoint = backupJob.RunJobObjectsWithoutDirectories();
            return restorePoint;
        }

        public void RemoveJobObject(string name, string backupJobName)
        {
            foreach (var backupJob in _backupJobs)
            {
                if (backupJob.Name.Equals(backupJobName))
                {
                    backupJob.RemoveJobObject(name);
                }
            }
        }

        public void RemoveBackupJob(string name)
        {
            foreach (var backupJob in _backupJobs)
            {
                if (backupJob.Name.Equals(name))
                {
                    _backupJobs.Remove(backupJob);
                    return;
                }
            }

            throw new BackupsException("can't find this BackupJob!");
        }

        public void AddPathToJobObject(string path, JobObject jobObject)
        {
            jobObject.AddPath(path);
        }

        public void ChangeBackupJobMode(BackupJob backupJob)
        {
            backupJob.ChangeMode();
        }

        public void RemoveJobObjectPath(JobObject jobObject, string path)
        {
            jobObject.DeletePath(path);
        }
    }
}