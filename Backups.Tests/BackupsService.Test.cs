using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Entities;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private IIBackup _iBackup;
        private IIRepository _iRepository;

        [SetUp]
        public void Setup()
        {
            _iBackup = new Ibackup();
            _iRepository = new IRepository();
        }

        [Test]
        public void MakeTwoJobObjects_RunBackupJob_DeleteSecondJobObject_RunBackupJob_ThereAreTwoRestorePoints_AndThreeStorages()
        {
            IIRepository irepository = new IRepository();
            Backup mainBackup = _iBackup.CreateBackup("mainBackup");
            BackupJob mainBackupJob = _iBackup.CreateBackupJob("mainBackupJob", irepository);
            List<string> aPathsList = new List<string>();
            List<string> bPathsList = new List<string>();
            JobObject a = _iBackup.CreateJobObject("FirstJobObject", aPathsList, mainBackupJob);
            JobObject b = _iBackup.CreateJobObject("SecondJobObject", aPathsList, mainBackupJob);
            RestorePoint first = _iBackup.RunBackupJob(mainBackupJob);
            _iBackup.RemoveJobObject("SecondJobObject", "mainBackupJob");
            RestorePoint second = _iBackup.RunBackupJob(mainBackupJob);
            Assert.AreEqual(2, mainBackupJob.RestorePoints.Count);
            Assert.AreEqual(3, first.Storages.Count + second.Storages.Count);
        }
    }
}
