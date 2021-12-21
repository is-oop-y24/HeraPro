using System.Collections.Generic;
using System.Runtime.Serialization;
using Backups.Database;

namespace Backups.Entity
{
    [DataContract]
    public class Backup
    {
        [DataMember]
        private readonly IRepository _repository;

        public Backup(IRepository rep)
        {
            _repository = rep;
        }

        public Backup()
        {
            _repository = new VirtualRepository();
        }

        public static BackupJob CreateBackupJob(IEnumerable<string> files, string path = "Resource")
        {
            return new BackupJob(files, path);
        }

        public void SaveBackup(BackupJob backupJob)
        {
            foreach (RestorePoint i in backupJob.GetRestorePoints())
            {
                _repository.Create(i);
            }
        }

        public void UpdateDatabase() => _repository.Save();
    }
}