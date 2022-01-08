using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Entity
{
    public class RestorePoint
    {
        internal RestorePoint(IEnumerable<Storage> zipFiles)
        {
            Time = DateTime.Now;
            ZipFiles = zipFiles.ToList();
        }

        internal RestorePoint(Storage pathToZipFile)
        {
            Time = DateTime.Now;
            ZipFiles = new List<Storage> { pathToZipFile };
        }

        public DateTime Time { get; }
        public List<Storage> ZipFiles { get; }
    }
}