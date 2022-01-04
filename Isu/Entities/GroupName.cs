using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        internal GroupName(string name)
        {
            string tmp = name[2].ToString();
            if (name is not { Length: 5 } || !int.TryParse(tmp, out _))
                throw new IsuException(IsuException.IncorrectGroupName);

            Name = name;
            MegaFaculty = name[..2];
        }

        public string MegaFaculty { get; }
        public string Name { get; }
    }
}