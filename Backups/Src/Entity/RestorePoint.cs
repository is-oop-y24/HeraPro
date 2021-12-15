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
            PathsToZipFiles = zipFiles.ToList();
        }

        internal RestorePoint(Storage pathToZipFile)
        {
            Time = DateTime.Now;
            PathsToZipFiles = new List<Storage> { pathToZipFile };
        }

        public DateTime Time { get; }
        public List<Storage> PathsToZipFiles { get; }
    }
}