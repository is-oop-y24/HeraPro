using System.Collections.Generic;
using System.Linq;

namespace Backups.Entity
{
    public class JobObject
    {
        internal JobObject(IEnumerable<string> files)
        {
            Files = files.ToList();
        }

        public List<string> Files { get; }
    }
}