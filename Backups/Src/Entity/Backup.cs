using System.Collections.Generic;
using Backups.Database;

namespace Backups.Entity
{
    public class Backup
    {
        private readonly Repository _repository;

        public Backup(IEnumerable<string> rep)
        {
            _repository = new Repository(rep);
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
    }
}