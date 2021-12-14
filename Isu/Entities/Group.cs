using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        internal Group(string name, int maxNumberOfStudentsPerGroup)
        {
            if (maxNumberOfStudentsPerGroup < 0)
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);

            GroupName = new GroupName(name);
            Course = new CourseNumber(int.Parse(name.Substring(2, 1)));

            MaxNumberOfStudentsPerGroup = maxNumberOfStudentsPerGroup;
        }

        public GroupName GroupName { get; }
        public CourseNumber Course { get; }
        public int MaxNumberOfStudentsPerGroup { get; }
    }
}