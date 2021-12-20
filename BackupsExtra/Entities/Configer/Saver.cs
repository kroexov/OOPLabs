using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.Configer
{
    public class Saver
    {
        private Configer _configer = new Configer();
        public void SaveBackupJob(ExtraBackupJob backupJob)
        {
            _configer.AddConfig("BackupJob: " + backupJob.Name);
            foreach (var jobObject in backupJob.BackupJob.JobObjects)
            {
                _configer.AddConfig("JobObject: " + jobObject.Name);
            }

            foreach (var restorePoint in backupJob.BackupJob.RestorePoints)
            {
                _configer.AddConfig("RestorePoint: " + restorePoint.Date);
                foreach (var storage in restorePoint.Storages)
                {
                    _configer.AddConfig("Storage: " + storage.Name);
                }
            }
        }
    }
}