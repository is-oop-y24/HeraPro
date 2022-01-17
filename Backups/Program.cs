using System;
using System.Collections.Generic;
using Backups.Algo;
using Backups.Database;
using Backups.Entity;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var files = new List<string>() { @"C:\Users\Khand\Desktop\test\file1.txt", @"C:\Users\Khand\Desktop\test\file2.txt", @"C:\Users\Khand\Desktop\test\file3.txt" };
            var singleStorage = new SingleStorage();
            var splitStorage = new SplitStorage();
            const string path = @"C:\Users\Khand\Desktop\output\";

            BackupJob backupJob1 = Backup.CreateBackupJob(files);

            backupJob1.AddNewRestorePoint(singleStorage);
            backupJob1.RemoveJobObject(@"C:\Users\Khand\Desktop\file3.txt");
            backupJob1.AddNewRestorePoint(splitStorage);
            var backup = new Backup(new LocalRepository(path));
            backup.SaveBackup(backupJob1);
            backup.UpdateDatabase();

            Console.WriteLine();
        }
    }
}