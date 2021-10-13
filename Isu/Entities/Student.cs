namespace Isu.Entities
{
    public class Student
    {
        public Student(int id, string name, GroupName group)
            : this(id, name)
        {
            Group = group;
        }

        private Student(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }

        public int Id { get; }

        public GroupName Group { get; }
    }
}