using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;

namespace BackupsExtra.Services
{
    public interface IExtraBackup
    {
        void SaveBackup();
        void LoadBackup();

        public ExtraBackupJob GetExtraBackupJob(string name);

        public JobObject CreateJobObject(string name, ExtraBackupJob backupJob);

        public JobObject CreateJobObject(string name, List<string> paths, ExtraBackupJob backupJob);

        public RestorePoint RunBackupJob(ExtraBackupJob backupJob);

        public RestorePoint RunBackupJobWithoutArchiving(ExtraBackupJob backupJob);

        public void RemoveJobObject(string name, string backupJobName);

        public void RemoveBackupJob(string name);

        public void ChangeBackupJobMode(ExtraBackupJob backupJob);

        public void RemoveJobObjectPath(JobObject jobObject, string path);

        public void CleanPoints(ExtraBackupJob backupJob);

        public void MergePoints(ExtraBackupJob backupJob, RestorePoint rp1, RestorePoint rp2);

        ExtraBackupJob CreateExtraBackupJob(string name, IRepository repository, Logger logger, Merger merger, BackupJob backupJob, PointCleaner pointCleaner, PointRestorer pointRestorer);
    }
}