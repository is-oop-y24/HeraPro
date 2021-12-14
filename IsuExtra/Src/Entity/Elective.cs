using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace IsuExtra.Entity
{
    public class Elective
    {
        internal Elective(string facultyId, int maxNumberOfStudents, IEnumerable<Group> stream)
            : this(facultyId, maxNumberOfStudents)
        {
            var tmpStream = stream.ToList();
            Stream.AddRange(tmpStream);
        }

        internal Elective(string facultyId, int maxNumberOfStudents)
        {
            FacultyId = facultyId[..2];
            MaxNumberOfStudents = maxNumberOfStudents;
            Stream = new List<Group>();
        }

        public List<Group> Stream { get; }
        public string FacultyId { get; }
        public int MaxNumberOfStudents { get; }
    }
}