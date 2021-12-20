using System.Collections.Generic;
using System.Linq;
using Backups.Algo;

namespace Backups.Entity
{
    public class BackupJob
    {
        private readonly string _nameOfZipFiles;
        private readonly List<RestorePoint> _restorePoints;
        private readonly JobObject _jobObject;

        internal BackupJob(JobObject jobObject, IEnumerable<RestorePoint> restorePoints, string nameOfZipFiles)
        {
            _jobObject = jobObject;
            _restorePoints = restorePoints.ToList();
            _nameOfZipFiles = nameOfZipFiles;
        }

        internal BackupJob(IEnumerable<string> files, string nameOfZipFiles)
        {
            _jobObject = new JobObject(files);
            _restorePoints = new List<RestorePoint>();
            _nameOfZipFiles = nameOfZipFiles;
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, _nameOfZipFiles));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string zipName)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, zipName));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string zipName, string name)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, zipName + name));
        }

        public void AddNewJobObject(IEnumerable<string> files)
        {
            _jobObject.Files.AddRange(files);
        }

        public void RemoveJobObject(string file)
        {
            _jobObject.Files.Remove(file);
        }

        public JobObject GetJobObject()
        {
            var toReturn = new JobObject(_jobObject.Files);
            return toReturn;
        }

        public string GetPath()
        {
            return _nameOfZipFiles;
        }

        public IEnumerable<RestorePoint> GetRestorePoints()
        {
            return _restorePoints.AsEnumerable();
        }
    }
}