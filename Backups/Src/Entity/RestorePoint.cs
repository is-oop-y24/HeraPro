using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Backups.Entity
{
    [DataContract]
    public class RestorePoint
    {
        internal RestorePoint(IEnumerable<Storage> zipFiles)
        {
            Time = DateTime.Now;
            ZipFiles = zipFiles.ToList();
        }

        internal RestorePoint(Storage pathToZipFile)
        {
            Time = DateTime.Now;
            ZipFiles = new List<Storage> { pathToZipFile };
        }

        [DataMember]
        public DateTime Time { get; internal set; }
        [DataMember]
        public List<Storage> ZipFiles { get; set; }
    }
}