using System.Collections.Generic;
using System.Linq;
using Backups.Entity;

namespace Backups.Algo
{
    public class SplitStorage : IAlgoStorage
    {
        public RestorePoint AddNewRestorePoint(IEnumerable<string> files, string zipPath)
        {
            IEnumerable<Storage> zipFiles = from i in files
                let updZipPath = zipPath + '_' + i[(i.LastIndexOf('/') + 1) ..]
                select new Storage(i, updZipPath);

            return new RestorePoint(zipFiles);
        }
    }
}