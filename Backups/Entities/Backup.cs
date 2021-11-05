using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public class Backup
    {
        private string _name;
        private List<BackupJob> _backupJobs = new List<BackupJob>();

        public Backup(string name)
        {
            _name = name;
        }
    }
}