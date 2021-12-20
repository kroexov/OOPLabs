using System;
using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Services;

namespace BackupsExtra.Entities.PointCleaners
{
    public class DateRelatedCleaner : PointCleaner
    {
        private DateTime _timeLimit;

        public DateRelatedCleaner(DateTime timeLimit)
        {
            _timeLimit = timeLimit;
        }

        public override void Clean(List<RestorePoint> restorePoints)
        {
            foreach (var restorePoint in restorePoints)
            {
                if (restorePoint.Date < _timeLimit)
                {
                    AddRestorePoint(restorePoint);
                }
            }
        }
    }
}