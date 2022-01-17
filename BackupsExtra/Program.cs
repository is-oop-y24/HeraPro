using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algo;
using Backups.Entity;
using BackupsExtra.Algo;
using BackupsExtra.Database;
using BackupsExtra.Entity;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var files = new List<string>() { @"C:\Users\Khand\Desktop\test\file1.txt", @"C:\Users\Khand\Desktop\test\file2.txt", @"C:\Users\Khand\Desktop\test\file3.txt" };
            var singleStorage = new SingleStorage();
            var splitStorage = new SplitStorage();
            var rpByCount = new RestorePointsByCount(1);
            const string path = @"C:\Users\Khand\Desktop\output\";

            BackupJobExtra bjExtra = new (Backup.CreateBackupJob(files));

            bjExtra.BackupJob.AddNewRestorePoint(singleStorage);
            bjExtra.BackupJob.RemoveJobObject(@"C:\Users\Khand\Desktop\file3.txt");
            var rpByDate = new RestorePointsByDate(DateTime.Now);
            bjExtra.BackupJob.AddNewRestorePoint(splitStorage);
            var rep = new LocRepository(path);
            var backupExtra = new BackupExtra(rep);
            backupExtra.Backup.SaveBackup(bjExtra.BackupJob);
            backupExtra.Backup.UpdateDatabase();

            bjExtra.Serilizate(path);

            var backupJobExtra = BackupJobExtra.Deserilizate(path);

            var algos = new List<IAlgoRestorePoint>() { rpByCount, rpByDate };
            backupJobExtra.SortAndCleanRestorePoints(algos);

            backupJobExtra.BackupJob.AddNewRestorePoint(splitStorage);
            backupJobExtra.MergeRestorePoints(backupJobExtra.BackupJob.RestorePoints[0], backupJobExtra.BackupJob.RestorePoints[1]);

            rep.ExtractRestorePoint(backupJobExtra.BackupJob.RestorePoints.First(), @"C:\Users\Khand\Desktop\output\RestorePoint0");
        }
    }
}
