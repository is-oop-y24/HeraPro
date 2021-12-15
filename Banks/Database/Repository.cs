using System;
using System.Collections.Generic;

namespace Banks.Database
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private List<T> _context;

        public Repository()
        {
            _context = new List<T>();
        }

        public IEnumerable<T> GetItemList()
        {
            return _context.AsReadOnly();
        }

        public T GetItem(int id)
        {
            return _context[id];
        }

        public void Add(T item)
        {
            _context.Add(item);
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void Update(T exItem, T newItem)
        {
            if (_context.Remove(exItem))
                _context.Add(newItem);
        }

        public void Delete(int id)
        {
            _context.RemoveAt(id);
        }

        public void Save()
        {
        }
    }
}