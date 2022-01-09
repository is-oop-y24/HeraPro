using System.Runtime.Serialization;
using Backups.Entity;
using BackupsExtra.Database;
using IRepository = Backups.Database.IRepository;

namespace BackupsExtra.Entity
{
    [DataContract]
    public class BackupExtra
    {
        public BackupExtra(string path)
        {
            Backup = new Backup(new LocRepository(path));
        }

        public BackupExtra(IRepository rep)
        {
            Backup = new Backup(rep);
        }

        public BackupExtra(Backup backup)
        {
            Backup = backup;
        }

        [DataMember]
        public Backup Backup { get; }
    }
}