using System.Collections.Generic;
using Backups.Entity;

namespace Backups.Database
{
    public interface IRepository
    {
        IEnumerable<Storage> GetItemList();
        IEnumerable<Storage> FindItem(string file);
        void Create(Storage item);
        void Delete(Storage item);
        void Save();
    }
}