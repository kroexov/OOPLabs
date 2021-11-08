using System.Collections.Generic;
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