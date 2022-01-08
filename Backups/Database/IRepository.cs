using System;
using System.Collections.Generic;
using Backups.Entity;

namespace Backups.Database
{
    public interface IRepository
    {
        IEnumerable<RestorePoint> GetItemList();
        IEnumerable<RestorePoint> FindItem(DateTime time);
        void Create(RestorePoint item);
        void Delete(RestorePoint item);
        void Save();
    }
}