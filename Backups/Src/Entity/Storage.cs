using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Backups.Entity
{
    [DataContract]
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

        [DataMember]
        public string ZipName { get; internal set; }
        [DataMember]
        public List<string> Directory { get; internal set; }
    }
}