using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        internal GroupName(string name)
        {
            if (name is not { Length: 5 })
                throw new IsuException(IsuException.IncorrectGroupName);

            Name = name;
        }

        public string Name { get; }
    }
}