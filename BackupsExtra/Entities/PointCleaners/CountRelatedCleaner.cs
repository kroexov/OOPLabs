using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.PointCleaners
{
    public class CountRelatedCleaner : PointCleaner
    {
        private int _maxCount;

        public CountRelatedCleaner(int maxCount)
        {
            _maxCount = maxCount;
        }

        public override void Clean(List<RestorePoint> restorePoints)
        {
            if (_maxCount > restorePoints.Count)
            {
                return;
            }

            for (int i = restorePoints.Count; i > _maxCount - 1; i--)
            {
                AddRestorePoint(restorePoints[i - 1]);
            }
        }
    }
}