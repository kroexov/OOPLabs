using System.Collections.Generic;
using Backups.Entities;

namespace BackupsExtra.Services
{
    public abstract class PointCleaner
    {
        private List<RestorePoint> _remainedPoints = new List<RestorePoint>();

        public List<RestorePoint> CleanedPoints
        {
            get => _remainedPoints;
        }

        public abstract void Clean(List<RestorePoint> restorePoints);

        public void AddRestorePoint(RestorePoint restorePoint)
        {
            _remainedPoints.Add(restorePoint);
        }
    }
}