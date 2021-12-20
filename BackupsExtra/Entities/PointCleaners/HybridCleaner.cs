using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.PointCleaners
{
    public class HybridCleaner : PointCleaner
    {
        private int _maxCount;
        private DateTime _timeLimit;

        public HybridCleaner(int maxCount, DateTime timeLimit)
        {
            _maxCount = maxCount;
            _timeLimit = timeLimit;
        }

        public override void Clean(List<RestorePoint> restorePoints)
        {
            if (_maxCount > restorePoints.Count)
            {
                foreach (var restorePoint in restorePoints)
                {
                    if (restorePoint.Date < _timeLimit)
                    {
                        AddRestorePoint(restorePoint);
                    }
                }

                return;
            }

            for (int i = restorePoints.Count; i > _maxCount - 1; i--)
            {
                if (restorePoints[i - 1].Date < _timeLimit)
                {
                    AddRestorePoint(restorePoints[i - 1]);
                }
            }
        }
    }
}