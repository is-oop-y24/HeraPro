using System.Collections.Generic;
using Backups.Entity;

namespace BackupsExtra.Algo
{
    public interface IAlgoRestorePoint
    {
        IEnumerable<RestorePoint> Compare(IEnumerable<RestorePoint> list);
    }
}