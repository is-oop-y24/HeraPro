using Isu.Tools;

namespace Isu.Entities
{
    public class Person
    {
        internal Person(int id, string name, Group group)
        {
            if (group == null)
                throw new IsuException(IsuException.NoGroups);

            Group = group;
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }
        public Group Group { get; }
    }
}