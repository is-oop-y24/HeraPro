using System.Collections.Generic;
using Backups.Entity;

namespace Backups.Algo
{
    public class SingleStorage : IAlgoStorage
    {
        public RestorePoint AddNewRestorePoint(IEnumerable<string> files, string zipPath)
        {
            return new RestorePoint(new Storage(files, zipPath));
        }
    }
}