using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<CourseNumber> _courseNumbers;
        private int _defaultId = 100000;

        public IsuService(int course)
        {
            if (course <= 0)
                throw new IsuException(IsuException.IncorrectCourseNumber);

            _courseNumbers = new List<CourseNumber>(course + 1);
            for (int i = 1; i <= course; ++i)
            {
                _courseNumbers.Add(new CourseNumber(i));
            }
        }

        public Group AddGroup(GroupName name)
        {
            if (name.Course >= _courseNumbers.Capacity)
                throw new IsuException(IsuException.IncorrectCourseNumber);
            if (_courseNumbers[name.Course].ListOfGroupsByCourse.Any(i => i.GroupName.Equals(name)))
                throw new IsuException(IsuException.AddSameGroupNameTwiceError);

            var newGroup = new Group(name);
            _courseNumbers[name.Course].ListOfGroupsByCourse.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.MaxNumberOfStudentsPerGroup.Equals(group.ListOfStudents.Count))
                throw new IsuException(IsuException.MaxStudentsPerGroupReached);

            var newStudent = new Student(_defaultId++, name, group.GroupName);
            group.ListOfStudents.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            if (id < 0)
                throw new IsuException(IsuException.NoStudentWithSuchId);

            return _courseNumbers
                .SelectMany(course => course.ListOfGroupsByCourse, (course, group) => new { course, group })
                .SelectMany(t => t.group.ListOfStudents).FirstOrDefault(student => student.Id.Equals(id));
        }

        public Student FindStudent(string name) =>
            _courseNumbers.SelectMany(course => course.ListOfGroupsByCourse)
                .SelectMany(group => group.ListOfStudents)
                .FirstOrDefault(student => student.Name.Equals(name));

        public List<Student> FindStudents(GroupName groupName)
        {
            if (groupName.Course >= _courseNumbers.Capacity)
                throw new IsuException(IsuException.IncorrectCourseNumber);

            return _courseNumbers[groupName.Course]
                .ListOfGroupsByCourse.Where(group => group.GroupName.Equals(groupName))
                .Select(group => group.ListOfStudents).FirstOrDefault();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            if (!courseNumber.ListOfGroupsByCourse.Any())
                throw new IsuException(IsuException.GroupIsNotFound);

            IEnumerable<Student> students = courseNumber.ListOfGroupsByCourse.SelectMany(s => s.ListOfStudents);
            return students.ToList();
        }

        public Group FindGroup(GroupName groupName)
        {
            if (groupName.Course >= _courseNumbers.Capacity)
                throw new IsuException(IsuException.IncorrectCourseNumber);

            return _courseNumbers[groupName.Course]
                .ListOfGroupsByCourse.FirstOrDefault(group => group.GroupName.Equals(groupName));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            if (courseNumber?.ListOfGroupsByCourse == null)
                throw new IsuException(IsuException.GroupIsNotFound);

            return courseNumber.ListOfGroupsByCourse;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student.Group.Course >= _courseNumbers.Capacity)
                throw new IsuException(IsuException.IncorrectCourseNumber);
            if (_courseNumbers[student.Group.Course]?.ListOfGroupsByCourse == null)
                throw new IsuException(IsuException.NoSuchGroup);
            if (!_courseNumbers[newGroup.GroupName.Course].ListOfGroupsByCourse.Contains(newGroup))
                throw new IsuException(IsuException.NoSuchGroup);

            Group currentGroup = _courseNumbers[student.Group.Course].ListOfGroupsByCourse
                .FirstOrDefault(g => g.ListOfStudents.Contains(student));
            if (currentGroup == null)
                throw new IsuException(IsuException.StudentIsNotFound);

            string name = student.Name;
            int id = student.Id;
            currentGroup.ListOfStudents.Remove(student);
            newGroup.ListOfStudents.Add(new Student(id, name, newGroup.GroupName));
        }
    }
}