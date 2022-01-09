using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        internal GroupName(string name)
        {
            if (name is not { Length: 5 })
                throw new IsuException(IsuException.IncorrectGroupName);

            string tmp = name[2].ToString();
            if (!int.TryParse(tmp, out _))
                throw new IsuException(IsuException.IncorrectGroupName);

            Name = name;
            MegaFaculty = name[..2];
        }

        public string MegaFaculty { get; }
        public string Name { get; }
    }
}