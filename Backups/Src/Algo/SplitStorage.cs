using System.Collections.Generic;
using System.Linq;
using Backups.Entity;

namespace Backups.Algo
{
    public class SplitStorage : IAlgoStorage
    {
        public RestorePoint AddNewRestorePoint(IEnumerable<string> files, string zipName)
        {
            int count = 0;
            var list = files.Select(file => new Storage(file, zipName + count++)).ToList();

            return new RestorePoint(list);
        }
    }
}