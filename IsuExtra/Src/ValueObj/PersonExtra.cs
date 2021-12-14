using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entity;

namespace IsuExtra.ValueObj
{
    public class PersonExtra
    {
        internal PersonExtra(Person person, Group electiveGroup1, Group electiveGroup2)
        {
            Person = person;
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