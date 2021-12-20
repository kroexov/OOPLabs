using System.Collections.Generic;
using System.Data;
using Backups.Entities;
using Backups.Services;
using BackupsExtra.Entities.Loggers;
using BackupsExtra.Entities.PointCleaners;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        private IBackup _iBackup;
        private IExtraBackup _iExtraBackup;
        private IRepository _iRepository;
        [SetUp]
        public void Setup()
        {
            _iBackup = new BackupService();
            _iExtraBackup = new ExtraBackupService();
            _iRepository = new RepositoryManager();
        }

        [Test]
        public void SetRestorePointsLimits_ClearRestorePoints()
        {
            BackupJob backupJob = _iBackup.CreateBackupJob("1", _iRepository);
            ExtraBackupJob extraBackupJob = _iExtraBackup.CreateExtraBackupJob("1", _iRepository, new ConsoleLogger(), new Merger(), backupJob,
                new CountRelatedCleaner(2), new PointRestorer());
            _iExtraBackup.CreateJobObject("a", extraBackupJob);
            _iExtraBackup.RunBackupJob(extraBackupJob);
            _iExtraBackup.CreateJobObject("b", extraBackupJob);
            _iExtraBackup.RunBackupJob(extraBackupJob);
            _iExtraBackup.CreateJobObject("c", extraBackupJob);
            _iExtraBackup.RunBackupJob(extraBackupJob);
            Assert.AreEqual(extraBackupJob.BackupJob.RestorePoints.Count, 3);
            _iExtraBackup.CleanPoints(extraBackupJob);
            Assert.AreEqual(extraBackupJob.BackupJob.RestorePoints.Count, 2);
        }
        
        [Test]
        public void MergeTwoRestorePoints()
        {
            BackupJob backupJob = _iBackup.CreateBackupJob("1", _iRepository);
            ExtraBackupJob extraBackupJob = _iExtraBackup.CreateExtraBackupJob("1", _iRepository, new ConsoleLogger(), new Merger(), backupJob,
                new CountRelatedCleaner(2), new PointRestorer());
            List<string> aPathsList = new List<string>() { @"\A" };
            List<string> bPathsList = new List<string>() { @"\B" };
            _iExtraBackup.CreateJobObject("a", aPathsList, extraBackupJob);
            RestorePoint restorePoint1 = _iExtraBackup.RunBackupJobWithoutArchiving(extraBackupJob);
            _iExtraBackup.CreateJobObject("b", bPathsList, extraBackupJob);
            RestorePoint restorePoint2 = _iExtraBackup.RunBackupJobWithoutArchiving(extraBackupJob);
            _iExtraBackup.MergePoints(extraBackupJob, restorePoint1, restorePoint2);
            Assert.AreEqual(extraBackupJob.BackupJob.RestorePoints.Count, 1);
            Assert.AreEqual(restorePoint2.Storages.Count, 2);
        }
    }
}