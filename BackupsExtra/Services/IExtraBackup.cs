using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;

namespace BackupsExtra.Services
{
    public interface IExtraBackup
    {
        void SaveBackup();
        void LoadBackup();

        ExtraBackupJob GetExtraBackupJob(string name);

        JobObject CreateJobObject(string name, ExtraBackupJob backupJob);

        JobObject CreateJobObject(string name, List<string> paths, ExtraBackupJob backupJob);

        RestorePoint RunBackupJob(ExtraBackupJob backupJob);

        RestorePoint RunBackupJobWithoutArchiving(ExtraBackupJob backupJob);

        void RemoveJobObject(string name, string backupJobName);

        void RemoveBackupJob(string name);

        void ChangeBackupJobMode(ExtraBackupJob backupJob);

        void RemoveJobObjectPath(JobObject jobObject, string path);

        void CleanPoints(ExtraBackupJob backupJob);

        void MergePoints(ExtraBackupJob backupJob, RestorePoint rp1, RestorePoint rp2);

        ExtraBackupJob CreateExtraBackupJob(string name, IRepository repository, Logger logger, Merger merger, BackupJob backupJob, PointCleaner pointCleaner, PointRestorer pointRestorer);
    }
}