using System.Collections.Generic;
using Backups.Database;

namespace Backups.Entity
{
    public class Backup
    {
        private readonly IRepository _repository;

        public Backup(IRepository rep)
        {
            _repository = rep;
        }

        public Backup()
        {
            _repository = new Repository();
        }

        public static BackupJob CreateBackupJob(IEnumerable<string> files, string path)
        {
            return new BackupJob(files, path);
        }

        public void SaveBackup(BackupJob backupJob)
        {
            foreach (RestorePoint i in backupJob.GetRestorePoints())
            {
                foreach (Storage j in i.PathsToZipFiles)
                    _repository.Create(j);
            }
        }

        public void UpdateDatabase() => _repository.Save();
    }
}