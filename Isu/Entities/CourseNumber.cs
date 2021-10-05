using System.Collections.Generic;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber()
        {
            ListOfGroupsByCourse = new List<Group>();
        }

        public CourseNumber(Group group)
        {
            ListOfGroupsByCourse = new List<Group> { group };
        }

        public List<Group> ListOfGroupsByCourse { get; set; }
    }
}