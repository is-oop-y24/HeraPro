using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entity
{
    public class Elective
    {
        internal Elective(string facultyId, int maxNumberOfStudents, IEnumerable<Group> stream)
            : this(facultyId, maxNumberOfStudents)
        {
            var tmpStream = stream.ToList();
            if (tmpStream.Count == 0)
                throw new IsuExtraException(IsuExtraException.ElectiveBuildException);

            Streams.AddRange(tmpStream);
        }

        internal Elective(string facultyId, int maxNumberOfStudents)
        {
            if (facultyId == null)
                throw new IsuExtraException(IsuExtraException.ElectiveBuildException);

            FacultyId = facultyId[..2];
            MaxNumberOfStudents = maxNumberOfStudents;
            Streams = new List<Group>();
        }

        public List<Group> Streams { get; }
        public string FacultyId { get; }
        public int MaxNumberOfStudents { get; }
    }
}