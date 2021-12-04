using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algo;
using Backups.Entity;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private Backup _backup;
        private List<string> _files1;
        private List<string> _files2;
        private List<string> _files3;
        private readonly string _path = "/test/";
        private SingleStorage _singleStorage;
        private SplitStorage _splitStorage;

        [SetUp]
        public void Setup()
        {
            _backup = new Backup();
            _files1 = new List<string> {"FileA", "FileB"};
            _files2 = new List<string> {"/home/fileABC", "/oop/something"};
            _files3 = new List<string> {"///", "@@@"};
            _singleStorage = new SingleStorage();
            _splitStorage = new SplitStorage();
        }

        [Test]
        public void Test1()
        {
            BackupJob backupJob = Backup.CreateBackupJob(_files1, _path);
            backupJob.AddNewRestorePoint(_splitStorage);
            backupJob.RemoveJobObject("FileB");
            backupJob.AddNewRestorePoint(_splitStorage);
            Assert.AreEqual(2, backupJob.GetRestorePoints().Count());
            IEnumerable<int> storages = backupJob.GetRestorePoints().Select(x => x.PathsToZipFiles.Count);
            int sum = storages.Sum();
            Assert.AreEqual(3, sum);
        }

        [Test]
        public void Test2()
        {
            BackupJob backupJob1 = Backup.CreateBackupJob(_files1, _path);
            BackupJob backupJob2 = Backup.CreateBackupJob(_files2, _path);
            BackupJob backupJob3 = Backup.CreateBackupJob(_files3, _path);

            backupJob1.AddNewRestorePoint(_singleStorage, "/", "backupJob1");
            backupJob2.AddNewRestorePoint(_singleStorage, "C:\\", "backupJob2");
            backupJob3.AddNewRestorePoint(_singleStorage);

            backupJob1.AddNewRestorePoint(_splitStorage);
            backupJob2.AddNewRestorePoint(_splitStorage);
            backupJob3.AddNewRestorePoint(_splitStorage);

            _backup.SaveBackup(backupJob1);
            _backup.SaveBackup(backupJob2);
            _backup.SaveBackup(backupJob3);

            Console.WriteLine();
        }
    }
}