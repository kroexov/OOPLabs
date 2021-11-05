using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Entities;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            string curDir = Directory.GetCurrentDirectory();
            IIBackup ibackup = new Ibackup();
            Directory.CreateDirectory(curDir + @"\BackupRoot");
            Directory.CreateDirectory(curDir + @"\A");
            FileInfo atxt = new FileInfo(curDir + @"\A\A.txt");
            FileStream afs = atxt.Create();
            afs.Write(Encoding.UTF8.GetBytes("I am A.txt"));
            afs.Close();
            Directory.CreateDirectory(curDir + @"\B");
            FileInfo btxt = new FileInfo(curDir + @"\B\B.txt");
            FileStream bfs = btxt.Create();
            bfs.Write(Encoding.UTF8.GetBytes("I am B.txt"));
            bfs.Close();
            IIRepository irepository = new IRepository();
            Backup mainBackup = ibackup.CreateBackup("mainBackup");
            BackupJob mainBackupJob = ibackup.CreateBackupJob("mainBackupJob", irepository);
            List<string> aPathsList = new List<string>() { curDir + @"\A" };
            List<string> bPathsList = new List<string>() { curDir + @"\B" };
            JobObject a = ibackup.CreateJobObject("FirstJob", aPathsList, mainBackupJob);
            JobObject b = ibackup.CreateJobObject("SecondJob", bPathsList, mainBackupJob);
            ibackup.ChangeBackupJobMode(mainBackupJob);
            RestorePoint first = ibackup.RunBackupJob(mainBackupJob);
            irepository.DeleteStorage(curDir + "/A");
            irepository.DeleteStorage(curDir + "/B");
        }
    }
}
