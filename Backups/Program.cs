using System.Collections.Generic;
using Backups.Algo;
using Backups.Entity;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var files = new List<string>() { @"/home/hera/Downloads/lab-04" };
            var singleStorage = new SingleStorage();
            var splitStorage = new SplitStorage();
            const string path = @"/home/hera/test";

            BackupJob backupJob1 = Backup.CreateBackupJob(files, path);

            backupJob1.AddNewRestorePoint(singleStorage);
            var backup = new Backup();
            backup.SaveBackup(backupJob1);
            backup.UpdateDatabase();
        }
    }
}