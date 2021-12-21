using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Algo;

namespace Backups.Entity
{
    [DataContract]
    public class BackupJob
    {
        public BackupJob(string nameOfZipFiles, List<RestorePoint> restorePoints, JobObject jobObject)
        {
            NameOfZipFiles = nameOfZipFiles;
            RestorePoints = restorePoints;
            JobObject = jobObject;
        }

        internal BackupJob(string nameOfZipFiles, IEnumerable<RestorePoint> restorePoints, JobObject jobObject)
        {
            NameOfZipFiles = nameOfZipFiles;
            RestorePoints = restorePoints.ToList();
            JobObject = jobObject;
        }

        internal BackupJob(IEnumerable<string> files, string nameOfZipFiles)
        {
            JobObject = new JobObject(files);
            RestorePoints = new List<RestorePoint>();
            NameOfZipFiles = nameOfZipFiles;
        }

        [DataMember]
        public string NameOfZipFiles { get; set; }
        [DataMember]
        public List<RestorePoint> RestorePoints { get; set; }
        [DataMember]
        public JobObject JobObject { get; set; }

        public void AddNewRestorePoint(IAlgoStorage algoStorage)
        {
            RestorePoints.Add(algoStorage.AddNewRestorePoint(JobObject.Files, NameOfZipFiles));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string zipName)
        {
            RestorePoints.Add(algoStorage.AddNewRestorePoint(JobObject.Files, zipName));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string zipName, string name)
        {
            RestorePoints.Add(algoStorage.AddNewRestorePoint(JobObject.Files, zipName + name));
        }

        public void AddNewJobObject(IEnumerable<string> files)
        {
            JobObject.Files.AddRange(files);
        }

        public void RemoveJobObject(string file)
        {
            JobObject.Files.Remove(file);
        }

        public JobObject GetJobObject()
        {
            var toReturn = new JobObject(JobObject.Files);
            return toReturn;
        }

        public string GetPath()
        {
            return NameOfZipFiles;
        }

        public IEnumerable<RestorePoint> GetRestorePoints()
        {
            return RestorePoints.AsEnumerable();
        }
    }
}