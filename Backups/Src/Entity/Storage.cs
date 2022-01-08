using System.Collections.Generic;
using System.Linq;

namespace Backups.Entity
{
    public class Storage
    {
        internal Storage(string pathToDirectory, string zipName)
        {
            Directory = new List<string> { pathToDirectory };
            ZipName = zipName;
        }

        internal Storage(IEnumerable<string> files, string zipName)
        {
            Directory = files.ToList();
            ZipName = zipName;
        }

        public string ZipName { get; }
        public List<string> Directory { get; }
    }
}