using System.Collections.Generic;
using Backups.Entity;

namespace Backups.Algo
{
    public interface IAlgoStorage
    {
        RestorePoint AddNewRestorePoint(IEnumerable<string> files, string zipName);
    }
}