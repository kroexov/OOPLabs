using Backups.Entities;
using Backups.Services;

namespace BackupsExtra.Services
{
    public class Merger
    {
        public void Merge(RestorePoint oldRestorePoint, RestorePoint newRestorePoint)
        {
            foreach (var oldStorage in oldRestorePoint.Storages)
            {
                bool nomatches = true;
                foreach (var newStorage in newRestorePoint.Storages)
                {
                    if (oldStorage.Paths.Equals(newStorage.Paths))
                    {
                        nomatches = false;
                    }
                }

                if (nomatches)
                {
                    newRestorePoint.Storages.Add(oldStorage);
                }
            }
        }
    }
}