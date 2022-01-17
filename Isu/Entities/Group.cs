using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        internal Group(string name, int maxNumberOfStudentsPerGroup, int courseId)
        {
            if (maxNumberOfStudentsPerGroup < 0)
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);
            if (name == null)
                throw new IsuException(IsuException.IncorrectGroupName);

            GroupName = new GroupName(name);

            var course = new CourseNumber(int.Parse(name.Substring(2, 1)));
            Course = course.Id <= courseId ? course : null;

            MaxNumberOfStudentsPerGroup = maxNumberOfStudentsPerGroup;
        }

        public GroupName GroupName { get; }
        public CourseNumber Course { get; }
        public int MaxNumberOfStudentsPerGroup { get; }
    }
}