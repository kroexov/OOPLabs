using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;
using BackupsExtra.Entities.Configer;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class ExtraBackupService : IExtraBackup
    {
        private List<ExtraBackupJob> _extraBackupJobs = new List<ExtraBackupJob>();
        private List<JobObject> _jobObjects = new List<JobObject>();
        private Saver _saver = new Saver();
        private string _configPath = Directory.GetCurrentDirectory() + @"\Config.txt";

        public ExtraBackupJob GetExtraBackupJob(string name)
        {
            foreach (var backupJob in _extraBackupJobs)
            {
                if (backupJob.Name.Equals(name))
                {
                    return backupJob;
                }
            }

            throw new BackupsExtraException("There's no backupJob with this name!");
        }

        public JobObject CreateJobObject(string name, ExtraBackupJob backupJob)
        {
            JobObject jobObject = new JobObject(name);
            backupJob.BackupJob.AddJobObject(jobObject);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public JobObject CreateJobObject(string name, List<string> paths, ExtraBackupJob backupJob)
        {
            JobObject jobObject = new JobObject(name, paths);
            backupJob.BackupJob.AddJobObject(jobObject);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public RestorePoint RunBackupJob(ExtraBackupJob backupJob)
        {
            RestorePoint restorePoint = backupJob.BackupJob.RunJobObjects();
            return restorePoint;
        }

        public RestorePoint RunBackupJobWithoutArchiving(ExtraBackupJob backupJob)
        {
            RestorePoint restorePoint = backupJob.BackupJob.RunJobObjectsWithoutDirectories();
            return restorePoint;
        }

        public void RemoveJobObject(string name, string backupJobName)
        {
            foreach (var backupJob in _extraBackupJobs)
            {
                if (backupJob.Name.Equals(backupJobName))
                {
                    backupJob.BackupJob.RemoveJobObject(name);
                }
            }
        }

        public void RemoveBackupJob(string name)
        {
            foreach (var backupJob in _extraBackupJobs)
            {
                if (backupJob.Name.Equals(name))
                {
                    _extraBackupJobs.Remove(backupJob);
                    return;
                }
            }

            throw new BackupsExtraException("can't find this BackupJob!");
        }

        public void ChangeBackupJobMode(ExtraBackupJob backupJob)
        {
            backupJob.BackupJob.ChangeMode();
        }

        public void RemoveJobObjectPath(JobObject jobObject, string path)
        {
            jobObject.DeletePath(path);
        }

        public void CleanPoints(ExtraBackupJob backupJob)
        {
            backupJob.CleanPoints();
        }

        public void MergePoints(ExtraBackupJob backupJob, RestorePoint rp1, RestorePoint rp2)
        {
            backupJob.MergePoints(rp1, rp2);
        }

        public void SaveBackup()
        {
            File.Delete(_configPath);
            File.Create(_configPath);
            foreach (var backupJob in _extraBackupJobs)
            {
                _saver.SaveBackupJob(backupJob);
            }
        }

        public void LoadBackup()
        {
            foreach (var line in File.ReadLines(_configPath))
            {
            }
        }

        public ExtraBackupJob CreateExtraBackupJob(string name, IRepository repository, Logger logger, Merger merger, BackupJob backupJob, PointCleaner pointCleaner, PointRestorer pointRestorer)
        {
            var extraBackupJobBuilder = new ExtraBackupJob.Builder();
            extraBackupJobBuilder
                .SetName(name)
                .SetRepository(repository)
                .SetLogger(logger)
                .SetMerger(merger)
                .SetBackupJob(backupJob)
                .SetPointCleaner(pointCleaner)
                .SetPointRestorer(pointRestorer);
            var extraBackupJob = extraBackupJobBuilder.Build();
            _extraBackupJobs.Add(extraBackupJob);
            return extraBackupJob;
        }
    }
}