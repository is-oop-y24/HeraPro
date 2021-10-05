using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string name)
        {
            if (name is not { Length: 5 } || name[..2] != "M3" || int.Parse(name.Substring(2, 1)) <= 0)
                throw new IsuException(IsuException.IncorrectGroupName);

            Name = name;
            Course = int.Parse(name.Substring(2, 1));
        }

        public int Course { get; }
        public string Name { get; }
    }
}