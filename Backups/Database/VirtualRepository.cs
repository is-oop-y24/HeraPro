using System;
using System.Collections.Generic;
using Backups.Entity;

namespace Backups.Database
{
    public class VirtualRepository : IRepository
    {
        private readonly List<RestorePoint> _context;
        private readonly string _path;

        public VirtualRepository(string path = "/")
        {
            _context = new List<RestorePoint>();
            _path = path;
        }

        public IEnumerable<RestorePoint> GetItemList()
        {
            return _context.AsReadOnly();
        }

        public IEnumerable<RestorePoint> FindItem(DateTime time)
        {
            return _context.FindAll(x => x.Time < time);
        }

        public void Create(RestorePoint item)
        {
            _context.Add(item);
        }

        public void Delete(RestorePoint item)
        {
            _context.Remove(item);
        }

        public void Save()
        {
            _context.Clear();
        }
    }
}