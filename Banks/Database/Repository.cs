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

        public void Add(T item)
        {
            _context.Add(item);
        }

        public void Update(T exItem, T newItem)
        {
            if (_context.Remove(exItem))
                _context.Add(newItem);
        }

        public void Remove(T item)
        {
            _context.Remove(item);
        }

        public bool Contains(T item)
        {
            return _context.Contains(item);
        }
    }
}