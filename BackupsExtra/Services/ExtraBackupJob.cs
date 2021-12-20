using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;
using BackupsExtra.Entities.Configer;

namespace BackupsExtra.Services
{
    public class ExtraBackupJob
    {
        private BackupJob _backupJob;
        private string _name;
        private Merger _merger;
        private PointCleaner _pointCleaner;
        private PointRestorer _pointRestorer;
        private Logger _logger;
        private IRepository _repository;

        private ExtraBackupJob()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public BackupJob BackupJob
        {
            get
            {
                return _backupJob;
            }
        }

        public void CleanPoints()
        {
            _pointCleaner.Clean(_backupJob.RestorePoints);
            _backupJob.RestorePoints = _pointCleaner.CleanedPoints;
        }

        public void MergePoints(RestorePoint oldrp, RestorePoint newrp)
        {
            _merger.Merge(oldrp, newrp);
            _backupJob.RestorePoints.Remove(oldrp);
        }

        private ExtraBackupJob SetName(string name)
        {
            _name = name;
            return this;
        }

        private ExtraBackupJob SetBackupJob(BackupJob backupJob)
        {
            _backupJob = backupJob;
            return this;
        }

        private ExtraBackupJob SetMerger(Merger merger)
        {
            _merger = merger;
            return this;
        }

        private ExtraBackupJob SetPointCleaner(PointCleaner pointCleaner)
        {
            _pointCleaner = pointCleaner;
            return this;
        }

        private ExtraBackupJob SetLogger(Logger logger)
        {
            _logger = logger;
            return this;
        }

        private ExtraBackupJob SetPointRestorer(PointRestorer pointRestorer)
        {
            _pointRestorer = pointRestorer;
            return this;
        }

        private ExtraBackupJob SetRepository(IRepository repository)
        {
            _repository = repository;
            return this;
        }

        public class Builder
        {
            private ExtraBackupJob _extraBackupJob = new ExtraBackupJob();

            public ExtraBackupJob.Builder SetName(string name)
            {
                _extraBackupJob.SetName(name);
                return this;
            }

            public ExtraBackupJob.Builder SetBackupJob(BackupJob backupJob)
            {
                _extraBackupJob.SetBackupJob(backupJob);
                return this;
            }

            public ExtraBackupJob.Builder SetMerger(Merger merger)
            {
                _extraBackupJob.SetMerger(merger);
                return this;
            }

            public ExtraBackupJob.Builder SetLogger(Logger logger)
            {
                _extraBackupJob.SetLogger(logger);
                return this;
            }

            public ExtraBackupJob.Builder SetPointCleaner(PointCleaner pointCleaner)
            {
                _extraBackupJob.SetPointCleaner(pointCleaner);
                return this;
            }

            public ExtraBackupJob.Builder SetPointRestorer(PointRestorer pointRestorer)
            {
                _extraBackupJob.SetPointRestorer(pointRestorer);
                return this;
            }

            public ExtraBackupJob.Builder SetRepository(IRepository repository)
            {
                _extraBackupJob.SetRepository(repository);
                return this;
            }

            public ExtraBackupJob Build()
            {
                return _extraBackupJob;
            }
        }
    }
}