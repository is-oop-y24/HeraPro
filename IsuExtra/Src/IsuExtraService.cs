using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entity;
using IsuExtra.TimeTable;
using IsuExtra.ValueObj;

namespace IsuExtra
{
    public class IsuExtraService
    {
        private readonly List<Elective> _electives;
        private readonly List<PersonExtra> _persons;
        private readonly IsuService _isuService;
        private readonly TimeTableService _timeTableService;
        private readonly Group _groupOfTeachers;

        public IsuExtraService()
        {
            _isuService = new IsuService();
            _persons = new List<PersonExtra>();
            _electives = new List<Elective>();
            _timeTableService = new TimeTableService();
            _groupOfTeachers = _isuService.AddGroup("11111", 300);
        }

        public PersonExtra AddStudentExtra(Person student)
        {
            var studentExtra = new PersonExtra(student);
            _persons.Add(studentExtra);

            return studentExtra;
        }

        public PersonExtra AddTeacher(string name)
        {
            var teacher = new PersonExtra(_isuService.AddStudent(name, _groupOfTeachers));
            _persons.Add(teacher);

            return teacher;
        }

        public Lesson AddLesson(DateTime start, DateTime end, PersonExtra teacher, int auditory) => new Lesson(start, end, teacher, auditory);

        public Elective AddElective(string faculty, int maxNumberOfStudents = 100)
        {
            var elective = new Elective(faculty, maxNumberOfStudents);
            _electives.Add(elective);

            return elective;
        }

        public bool AddGroupToElective(Group group, Elective elective)
        {
            if (!_electives.Contains(elective)) return false;
            elective.Stream.Add(group);
            return true;
        }

        public bool AddStudentToElectiveGroup(PersonExtra student, Group group)
        {
            if (student.ElectivesGroup.Count < 2)
                student.ElectivesGroup.Add(group);
            return student.ElectivesGroup.Contains(group);
        }

        public IEnumerable<PersonExtra> GetStudentsFromElectiveByStream(Elective elective)
        {
            return (from i in _persons from j in elective.Stream where i.ElectivesGroup.Contains(j) select i).ToList();
        }

        public IEnumerable<PersonExtra> FindStudentsByElectiveByStreamByGroup(Group group)
        {
            return _persons.Where(i => i.ElectivesGroup.Contains(@group)).ToList();
        }

        public PersonExtra RemoveStudentFromElective(PersonExtra student, Elective elective)
        {
            List<Group> list = student.ElectivesGroup;
            foreach (Group i in elective.Stream.Where(i => list.Contains(i)))
                list.Remove(i);
            Group id = list.Count == 0 ? null : list.First();
            var studentToReturn = new PersonExtra(student.Person, id);
            _persons.Remove(student);
            _persons.Add(studentToReturn);

            return studentToReturn;
        }

        public IEnumerable<PersonExtra> GetStudentsWithoutElectives() => _persons.FindAll(x => x.ElectivesGroup.Count < 2);

        public Group AddGroup(string name, int maxNumberOfStudentsPerGroup = 25)
        {
            return name != _groupOfTeachers.GroupName.Name ? _isuService.AddGroup(name, maxNumberOfStudentsPerGroup) : null;
        }

        public Person AddStudent(string name, Group group) => _isuService.AddStudent(name, group);

        public Person ChangeStudentGroup(Person person, Group newGroup) => _isuService.ChangeStudentGroup(person, newGroup);

        public Person FindPersonByName(string name) => _isuService.FindPersonByName(name);

        public IEnumerable<Person> FindStudentsByGroup(string group) => _isuService.FindStudentsByGroup(@group);

        public IEnumerable<Person> FindStudentsByCourse(int courseNumber) => _isuService.FindStudentsByCourse(courseNumber);

        public Group FindGroup(string group) => _isuService.FindGroup(@group);

        public IEnumerable<Group> FindGroups(int courseNumber) => _isuService.FindGroups(courseNumber);

        public IEnumerable<Person> GetAllPersons() => _isuService.GetAllPersons();

        public IEnumerable<Person> GetStudentsByGroups(Group group) => _isuService.GetStudentsByGroups(@group);

        public IEnumerable<Group> GetAllGroups() => _isuService.GetAllGroups();
    }
}