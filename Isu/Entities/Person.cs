namespace Isu.Entities
{
    public class Person
    {
        internal Person(int id, string name, Group group)
        {
            Group = group;
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }
        public Group Group { get; }
    }
}