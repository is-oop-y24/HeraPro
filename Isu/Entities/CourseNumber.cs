using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(int course)
        {
            Course = course;
            ListOfGroupsByCourse = new List<Group>();
        }

        public CourseNumber(int course, IReadOnlyCollection<Group> group)
            : this(course)
        {
            if (!group.Any())
                throw new IsuException(IsuException.NoGroups);

            ListOfGroupsByCourse.AddRange(group);
        }

        public int Course { get; }
        public List<Group> ListOfGroupsByCourse { get; }
    }
}