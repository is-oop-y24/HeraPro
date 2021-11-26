using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entity;

namespace IsuExtra.Service
{
    public class IsuExtraService
    {
        private readonly List<ExtraPerson> _persons;
        private readonly List<Elective> _electives;
        private IsuService _isuService;

        public IsuExtraService()
        {
            _isuService = new IsuService();
            _persons = new List<ExtraPerson>();
            _electives = new List<Elective>();
        }

        public Group AddGroup(string name, int maxNumberOfStudentsPerGroup = 25)
        {
            return _isuService.AddGroup(name, maxNumberOfStudentsPerGroup);
        }

        public ExtraPerson AddStudent(string name, Group group)
        {
            var student = new ExtraPerson(_isuService.AddStudent(name, group));
            _persons.Add(student);
            return student;
        }

        public Elective AddElective(string name, int maxNumberOfStudentsPerGroup = 100)
        {
            var elective = new Elective(name, maxNumberOfStudentsPerGroup);
            _electives.Add(elective);
            return elective;
        }

        public void RemoveElective(ExtraPerson person, Elective elective)
        {
            person.ElectivesId.Remove(elective.FacultyId);
        }

        public Person ChangeStudentGroup(Person person, Group newGroup)
        {
            return _isuService.ChangeStudentGroup(person, newGroup);
        }

        public Person FindPersonByName(string name)
        {
            return _isuService.FindPersonByName(name);
        }

        public IEnumerable<Person> FindStudentsByGroup(string @group)
        {
            return _isuService.FindStudentsByGroup(@group);
        }

        public IEnumerable<Person> FindStudentsByCourse(int courseNumber)
        {
            return _isuService.FindStudentsByCourse(courseNumber);
        }

        public Group FindGroup(string @group)
        {
            return _isuService.FindGroup(@group);
        }

        public IEnumerable<Group> FindGroups(int courseNumber)
        {
            return _isuService.FindGroups(courseNumber);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _isuService.GetAllPersons();
        }

        public IEnumerable<Person> GetStudentsByGroups(Group @group)
        {
            return _isuService.GetStudentsByGroups(@group);
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _isuService.GetAllGroups();
        }

        public IEnumerable<Elective> GetAllStreamsByCourse(int course)
        {
            return _electives.FindAll(x => x.Stream[0].Course.Id.Equals(course));
        }

        public IEnumerable<Person> GetAllStudentsByGroupByElective(Group group)
        {
            return _isuService.FindStudentsByGroup(group.GroupName.Name);
        }

        public IEnumerable<ExtraPerson> GetAllStudentsOutOfElectives()
        {
            return _persons.FindAll(x => x.ElectivesId.Count != 2);
        }
    }
}