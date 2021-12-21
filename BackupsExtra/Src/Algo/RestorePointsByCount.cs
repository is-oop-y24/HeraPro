using System.Collections.Generic;
using System.Linq;
using Backups.Entity;

namespace BackupsExtra.Algo
{
    public class RestorePointsByCount : IAlgoRestorePoint
    {
        public RestorePointsByCount(int count)
        {
            if (count != 0)
                Count = count;
        }

        public int Count { get; }

        public IEnumerable<RestorePoint> Compare(IEnumerable<RestorePoint> list)
        {
            var restorePoints = list.ToList();
            return restorePoints.Count <= Count ? restorePoints : restorePoints.Skip(restorePoints.Count - Count);
        }
    }
}