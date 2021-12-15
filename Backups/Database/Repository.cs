using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Backups.Entity;

namespace Backups.Database
{
    public class Repository : IRepository
    {
        private readonly List<Storage> _context;

        internal Repository()
        {
            _context = new List<Storage>();
        }

        internal Repository(IEnumerable<Storage> context)
        {
            _context = context.ToList();
        }

        public IEnumerable<Storage> GetItemList()
        {
            return _context.AsReadOnly();
        }

        public IEnumerable<Storage> FindItem(string file)
        {
            return _context.FindAll(x => x.Directory.Contains(file));
        }

        public void Create(Storage item)
        {
            _context.Add(item);
        }

        public void Delete(Storage item)
        {
            _context.Remove(item);
        }

        public void Save()
        {
            foreach (Storage i in _context)
            {
                ZipArchive archive = ZipFile.Open(i.ZipPath + ".zip", ZipArchiveMode.Create);
                foreach (string j in i.Directory)
                {
                    archive.CreateEntry(j);
                }
            }

            _context.Clear();
        }
    }
}