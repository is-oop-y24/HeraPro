using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Entity;
using BackupsExtra.Algo;

namespace BackupsExtra.Entity
{
    [DataContract]
    public class BackupJobExtra
    {
        internal BackupJobExtra(BackupJob backupJob)
        {
            BackupJob = backupJob;
        }

        [DataMember]
        public BackupJob BackupJob { get; private set; }

        public static BackupJobExtra Deserilizate(string path)
        {
            var ds = new DataContractSerializer(typeof(BackupJobExtra));
            using (Stream stream = File.OpenRead(path + "data.xml"))
            {
                var backupJobExtra = (BackupJobExtra)ds.ReadObject(stream);
                return backupJobExtra;
            }
        }

        public void Serilizate(string path)
        {
            var ds = new DataContractSerializer(typeof(BackupJobExtra));
            using (Stream stream = File.Create(path + "data.xml"))
                ds.WriteObject(stream, this);
        }

        public void SortAndCleanRestorePoints(List<IAlgoRestorePoint> algos)
        {
            if (algos.Count == 0)
                return;

            var list = algos.First().Compare(BackupJob.GetRestorePoints()).ToList();

            if (algos.Count > 1)
            {
                for (int i = 1; i < algos.Count(); ++i)
                {
                    var tmp = algos[i].Compare(BackupJob.GetRestorePoints()).ToList();
                    list = list.Intersect(tmp).ToList();
                }
            }

            BackupJob = new BackupJob(BackupJob.NameOfZipFiles, list, BackupJob.JobObject);
        }

        public void MergeRestorePoints(RestorePoint point1, RestorePoint point2)
        {
            if (point1.ZipFiles.Count == 1)
                point2.ZipFiles.AddRange(point1.ZipFiles);
            else if (point2.ZipFiles.Count == 1)
                point1.ZipFiles.AddRange(point2.ZipFiles);

            point1.ZipFiles = point2.ZipFiles.Except(point1.ZipFiles).ToList();
        }
    }
}