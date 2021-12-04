using System.Collections.Generic;
using System.Linq;

namespace Backups.Entity
{
    public class Storage
    {
        internal Storage(string pathToDirectory, string zipPath)
        {
            Directory = new List<string> { pathToDirectory };
            ZipPath = zipPath;
        }

        internal Storage(IEnumerable<string> files, string zipPath)
        {
            Directory = files.ToList();
            ZipPath = zipPath;
        }

        public string ZipPath { get; }
        public List<string> Directory { get; }
    }
}