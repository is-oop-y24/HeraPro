using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entity;

namespace Backups.Database
{
    public class LocalRepository : IRepository
    {
        private readonly List<RestorePoint> _context;
        private readonly string _path;
        private readonly string _nameOfRestorePoints;

        internal LocalRepository(string path, string nameOfRestorePoints = "RestorePoint")
        {
            _context = new List<RestorePoint>();
            if (Directory.Exists(path))
                _path = path;
            _nameOfRestorePoints = nameOfRestorePoints;
        }

        internal LocalRepository(IEnumerable<RestorePoint> context)
        {
            _context = context.ToList();
        }

        public IEnumerable<RestorePoint> GetItemList()
        {
            return _context.AsReadOnly();
        }

        public IEnumerable<RestorePoint> FindItem(DateTime time)
        {
            return _context.FindAll(x => x.Time < time);
        }

        public void Create(RestorePoint item)
        {
            _context.Add(item);
        }

        public void Delete(RestorePoint item)
        {
            _context.Remove(item);
        }

        public void Save()
        {
            int count = 0;
            string templateNameRp = _path + _nameOfRestorePoints;
            foreach (RestorePoint resPoint in _context)
            {
                DirectoryInfo pathToCurRestorePoint = Directory.CreateDirectory(templateNameRp + count++);
                foreach (Storage files in resPoint.ZipFiles)
                {
                    DirectoryInfo tmpDir = Directory.CreateDirectory(pathToCurRestorePoint.FullName + @"\tmp");
                    UniteFiles(files, tmpDir.FullName);
                    ZipFile.CreateFromDirectory(tmpDir.FullName, pathToCurRestorePoint.FullName + @"\" + files.ZipName + ".zip");

                    tmpDir.Delete(true);
                }
            }

            _context.Clear();
        }

        private void UniteFiles(Storage files, string targetPath)
        {
            foreach (string sourcePath in files.Directory)
            {
                File.Copy(sourcePath, targetPath + @"\" + sourcePath[(sourcePath.LastIndexOf(@"\", StringComparison.Ordinal) + 1) ..]);
            }
        }
    }
}