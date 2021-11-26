using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entity
{
    public class ExtraPerson
    {
        internal ExtraPerson(Person person, Elective elective1, Elective elective2)
        {
            Person = person;
        }

        internal ExtraPerson(Person person, Elective elective1)
         : this(person)
        {
            Person = person;
            ElectivesId.Add(elective1.FacultyId);
        }

        internal ExtraPerson(Person person)
        {
            Person = person;
            ElectivesId = new List<string>();
        }

        public Person Person { get; }
        public List<string> ElectivesId { get; }
    }
}