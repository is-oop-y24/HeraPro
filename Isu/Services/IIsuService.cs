using System.Collections.Generic;
using Isu.Entities;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(string name, int maxNumberOfStudentsPerGroup = 25);
        Person AddStudent(string name, Group group);

        Person ChangeStudentGroup(Person person, Group newGroup);

        Person FindPersonByName(string name);
        IEnumerable<Person> FindStudentsByGroup(string group);
        IEnumerable<Person> FindStudentsByCourse(int courseNumber);
        Group FindGroup(string group);
        IEnumerable<Group> FindGroups(int courseNumber);

        IEnumerable<Person> GetAllPersons();
        IEnumerable<Person> GetStudentsByGroups(Group group);
        IEnumerable<Group> GetAllGroups();
    }
}