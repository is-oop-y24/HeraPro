using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backups.Entity;

namespace Backups.Database
{
    public class Repository
    {
        private readonly List<string> _context;

        internal Repository()
        {
            _context = new List<string> { "/" };
        }

        internal Repository(IEnumerable<string> context)
        {
            _context = context.ToList();
        }

        public IEnumerable GetItemList()
        {
            return _context.AsReadOnly();
        }

        public IEnumerable<string> FindItem(string file)
        {
            return _context.FindAll(x => x.Contains("file"));
        }

        public void Create(Storage item)
        {
            _context.Add(item.ZipPath);
        }

        public void Delete(Storage item)
        {
            _context.Remove(item.ZipPath);
        }
    }
}