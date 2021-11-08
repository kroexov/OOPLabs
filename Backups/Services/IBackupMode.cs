using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public interface IBackupMode
    {
        RestorePoint RunJobObject(IRepository repository, List<JobObject> jobObjects, int restorePointsCount);
        RestorePoint RunJobObjectWithoutArchiving(IRepository repository, List<JobObject> jobObjects, int restorePointsCount);
        string GetMode();
    }
}