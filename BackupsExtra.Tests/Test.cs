using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algo;
using Backups.Database;
using Backups.Entity;
using BackupsExtra.Algo;
using BackupsExtra.Entity;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class Test
    {
        private IAlgoStorage _singleStorage;
        private IAlgoStorage _splitStorage;
        private IAlgoRestorePoint _rpByCount;
        private IAlgoRestorePoint _rpByDate;
        private BackupJobExtra _bjExtra;

        private List<string> _files = new List<string>()
        {
            @"C:\Users\Khand\Desktop\test\file1.txt",
            @"C:\Users\Khand\Desktop\test\file2.txt",
            @"C:\Users\Khand\Desktop\test\file3.txt"
        };
        
        private Backup _backup;
        private List<string> _files1;
        private List<string> _files2;
        private List<string> _files3;
        private readonly string _path = "/test/";
        
        [SetUp]
        public void Setup()
        {
            _backup = new Backup();
            _files1 = new List<string> {"FileA", "FileB"};
            _files2 = new List<string> {"/home/fileABC", "/oop/something"};
            _files3 = new List<string> {"///", "@@@"};
            _singleStorage = new SingleStorage();
            _splitStorage = new SplitStorage();
            _rpByCount = new RestorePointsByCount(1);
        }

        [Test]
        public void Test1FromBackup()
        {
            BackupJob backupJob = Backup.CreateBackupJob(_files1, _path);
            backupJob.AddNewRestorePoint(_splitStorage);
            backupJob.RemoveJobObject("FileB");
            backupJob.AddNewRestorePoint(_splitStorage);
            Assert.AreEqual(2, backupJob.GetRestorePoints().Count());
            IEnumerable<int> storages = backupJob.GetRestorePoints().Select(x => x.ZipFiles.Count);
            int sum = storages.Sum();
            Assert.AreEqual(3, sum);
        }

        [Test]
        public void Test2FromBackup()
        {
            BackupJob backupJob1 = Backup.CreateBackupJob(_files1, _path);
            BackupJob backupJob2 = Backup.CreateBackupJob(_files2, _path);
            BackupJob backupJob3 = Backup.CreateBackupJob(_files3, _path);

            backupJob1.AddNewRestorePoint(_singleStorage, "/", "backupJob1");
            backupJob2.AddNewRestorePoint(_singleStorage, @"C:\", "backupJob2");
            backupJob3.AddNewRestorePoint(_singleStorage);

            backupJob1.AddNewRestorePoint(_splitStorage);
            backupJob2.AddNewRestorePoint(_splitStorage);
            backupJob3.AddNewRestorePoint(_splitStorage);

            _backup.SaveBackup(backupJob1);
            _backup.SaveBackup(backupJob2);
            _backup.SaveBackup(backupJob3);

            Console.WriteLine();
        }

        [Test]
        public void SortAndCleanRestorePoints()
        {
            _bjExtra = new (Backup.CreateBackupJob(_files));
            _bjExtra.BackupJob.AddNewRestorePoint(_singleStorage);
            _bjExtra.BackupJob.RemoveJobObject(@"C:\Users\Khand\Desktop\file3.txt");
            _rpByDate = new RestorePointsByDate(DateTime.Now);
            _bjExtra.BackupJob.AddNewRestorePoint(_splitStorage);
            var rep = new VirtualRepository(_path);
            var backupExtra = new BackupExtra(rep);
            backupExtra.Backup.SaveBackup(_bjExtra.BackupJob);
            backupExtra.Backup.UpdateDatabase();
            var algos = new List<IAlgoRestorePoint>() { _rpByCount, _rpByDate };
            Assert.AreEqual(2,_bjExtra.BackupJob.RestorePoints.Count);
            _bjExtra.SortAndCleanRestorePoints(algos);
            Assert.AreEqual(1,_bjExtra.BackupJob.RestorePoints.Count);
            _bjExtra.BackupJob.AddNewRestorePoint(_splitStorage);
            _bjExtra.MergeRestorePoints(_bjExtra.BackupJob.RestorePoints[0], _bjExtra.BackupJob.RestorePoints[1]);
            Assert.AreEqual(3,_bjExtra.BackupJob.RestorePoints[0].ZipFiles.Count);
        }
    }
}