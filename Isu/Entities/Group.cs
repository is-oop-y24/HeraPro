using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        public Group(GroupName groupName)
        {
            GroupName = groupName;
        }

        public Group(GroupName groupName, Student student)
            : this(groupName)
        {
            ListOfStudents = new List<Student>(MaxNumberOfStudentsPerGroup) { student };
        }

        public Group(GroupName groupName, Student student, int maxNumberOfStudentsPerGroup)
            : this(groupName, maxNumberOfStudentsPerGroup)
        {
            ListOfStudents.Add(student);
        }

        public Group(GroupName groupName, int maxNumberOfStudentsPerGroup)
        : this(groupName)
        {
            if (maxNumberOfStudentsPerGroup < 0)
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);

            ListOfStudents = new List<Student>(MaxNumberOfStudentsPerGroup = maxNumberOfStudentsPerGroup);
        }

        public List<Student> ListOfStudents { get; set; } = new ();
        public GroupName GroupName { get; }

        public int MaxNumberOfStudentsPerGroup { get; set; } = 25;
    }
}