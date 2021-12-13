using System;
using System.Collections.Generic;

namespace Banks.Database
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetItemList();
        T GetItem(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}