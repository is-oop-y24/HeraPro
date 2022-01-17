using System.Collections.Generic;

namespace Banks.Database
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetItemList();
        void Add(T item);
        void Update(T exItem, T newItem);
        void Remove(T item);
        bool Contains(T item);
    }
}