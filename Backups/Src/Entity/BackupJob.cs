using System.Collections.Generic;
using System.Linq;
using Backups.Algo;

namespace Backups.Entity
{
    public class BackupJob
    {
        private readonly string _path;
        private readonly List<RestorePoint> _restorePoints;
        private JobObject _jobObject;

        internal BackupJob(JobObject jobObject, IEnumerable<RestorePoint> restorePoints, string path)
        {
            _jobObject = jobObject;
            _restorePoints = restorePoints.ToList();
            _path = path;
        }

        internal BackupJob(IEnumerable<string> files, string path)
        {
            _jobObject = new JobObject(files);
            _restorePoints = new List<RestorePoint>();
            _path = path;
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, _path + _restorePoints.Count));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string path)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, path + _restorePoints.Count));
        }

        public void AddNewRestorePoint(IAlgoStorage algoStorage, string path, string name)
        {
            _restorePoints.Add(algoStorage.AddNewRestorePoint(_jobObject.Files, path + name));
        }

        public void AddNewJobObject(IEnumerable<string> files)
        {
            _jobObject = new JobObject(files);
        }

        public void RemoveJobObject(string file)
        {
            _jobObject.Files.Remove(file);
        }

        public JobObject GetJobObject()
        {
            var tmp = new JobObject(_jobObject.Files);
            return tmp;
        }

        public string GetPath()
        {
            return _path;
        }

        public IEnumerable<RestorePoint> GetRestorePoints()
        {
            return _restorePoints.AsEnumerable();
        }
    }
}