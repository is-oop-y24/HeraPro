using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entity;

namespace BackupsExtra.Algo
{
    public class RestorePointsByDate : IAlgoRestorePoint
    {
        public RestorePointsByDate(DateTime time)
        {
            Time = time;
        }

        public DateTime Time { get; }

        public IEnumerable<RestorePoint> Compare(IEnumerable<RestorePoint> list)
        {
            return list.Where(rp => Time <= rp.Time);
        }
    }
}