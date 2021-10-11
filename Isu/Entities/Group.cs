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

        public Group(GroupName groupName, int maxNumberOfStudentsPerGroup)
        : this(groupName)
        {
            if (maxNumberOfStudentsPerGroup < 0)
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);

            MaxNumberOfStudentsPerGroup = maxNumberOfStudentsPerGroup;
            ListOfStudents = new List<Student>(MaxNumberOfStudentsPerGroup);
        }

        public List<Student> ListOfStudents { get; } = new ();
        public GroupName GroupName { get; }

        public int MaxNumberOfStudentsPerGroup { get; set; } = 25;
    }
}