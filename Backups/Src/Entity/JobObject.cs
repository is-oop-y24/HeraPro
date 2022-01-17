using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Backups.Entity
{
    [DataContract]
    public class JobObject
    {
        internal JobObject(IEnumerable<string> files)
        {
            Files = files.ToList();
        }

        [DataMember]
        public List<string> Files { get; internal set; }
    }
}