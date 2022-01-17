using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.ValueObj
{
    public class PersonExtra
    {
        internal PersonExtra(Person person, Group electiveGroup1, Group electiveGroup2)
        {
            if (person == null || electiveGroup1 == null || electiveGroup2 == null)
                throw new IsuExtraException(IsuExtraException.PersonExtraBuildException);

            Person = person;
            ElectivesGroup = new List<Group>() { electiveGroup1, electiveGroup2 };
        }

        internal PersonExtra(Person person, Group electiveGroup)
            : this(person)
        {
            Person = person;
            ElectivesGroup.Add(electiveGroup);
        }

        internal PersonExtra(Person person)
        {
            Person = person;
            ElectivesGroup = new List<Group>();
        }

        public Person Person { get; }
        public List<Group> ElectivesGroup { get; }
    }
}