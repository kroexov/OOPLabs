using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public interface IBackup
    {
        BackupJob CreateBackupJob(string name, IRepository repository);
        BackupJob GetBackupJob(string name);
        Backup CreateBackup(string name);
        JobObject CreateJobObject(string name, BackupJob backupJob);
        JobObject CreateJobObject(string name, List<string> paths, BackupJob backupJob);
        RestorePoint RunBackupJob(BackupJob backupJob);
        RestorePoint RunBackupJobWithoutArchiving(BackupJob backupJob);
        void RemoveJobObject(string name, string backupJobName);
        void RemoveBackupJob(string name);
        void ChangeBackupJobMode(BackupJob backupJob);
        void RemoveJobObjectPath(JobObject jobObject, string path);
    }
}