using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly Dictionary<Group, List<Person>> _studentsByGroup;
        private int _defaultIdOfStudent = 100000;
        private int _courseId;

        public IsuService(int courseId)
        {
            _studentsByGroup = new Dictionary<Group, List<Person>>();
            _courseId = courseId;
        }

        public Group AddGroup(string name, int maxNumberOfStudentsPerGroup = 25)
        {
            if (name == null)
                return null;
            Group check = FindGroup(name);
            if (check != null)
                return null;

            Group group = new (name, maxNumberOfStudentsPerGroup, _courseId);
            _studentsByGroup.Add(group, new List<Person>());
            return group;
        }

        public Person AddStudent(string name, Group group)
        {
            if (name == null || group == null)
                return null;
            if (_studentsByGroup[group].Count >= group.MaxNumberOfStudentsPerGroup)
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);

            _studentsByGroup[group].Add(new Person(_defaultIdOfStudent++, name, group));
            return _studentsByGroup[group][^1];
        }

        public Person ChangeStudentGroup(Person person, Group newGroup)
        {
            if (person == null || newGroup == null)
                return null;
            int index = _studentsByGroup[person.Group].IndexOf(person);
            if (index == -1)
                return null;

            var newStudent = new Person(person.Id, person.Name, newGroup);
            _studentsByGroup[person.Group].RemoveAt(index);
            _studentsByGroup[newGroup].Add(newStudent);
            return newStudent;
        }

        public Person FindPersonByName(string name) =>
            _studentsByGroup.Values.Select(x => x.Find(y => y.Name.Equals(name))).FirstOrDefault();

        public IEnumerable<Person> FindStudentsByGroup(string group) => _studentsByGroup[FindGroup(@group)];

        public IEnumerable<Person> FindStudentsByCourse(int courseNumber)
        {
            if (courseNumber < 1)
                return null;

            var result = new List<Person>();
            foreach (KeyValuePair<Group, List<Person>> keyValuePair in _studentsByGroup.Where(i =>
                i.Key.Course.Id.Equals(courseNumber)))
            {
                result.AddRange(keyValuePair.Value);
            }

            return result;
        }

        public Group FindGroup(string group) => _studentsByGroup.Keys.FirstOrDefault(x => x.GroupName.Name.Equals(group));

        public IEnumerable<Group> FindGroups(int courseNumber) =>
            courseNumber < 1
                ? null
                : _studentsByGroup.Keys.Where(i => i.Course.Id.Equals(courseNumber));

        public IEnumerable<Person> GetAllPersons() => _studentsByGroup.SelectMany(x => x.Value);
        public IEnumerable<Person> GetStudentsByGroups(Group group) => _studentsByGroup[group];
        public IEnumerable<Group> GetAllGroups() => _studentsByGroup.Keys;
    }
}