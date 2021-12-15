using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entity;
using IsuExtra.Service;
using IsuExtra.ValueObj;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private const string GroupM3208 = "M3208";
        private const string GroupM3310 = "M3310";
        private const string GroupExtraName1 = "M3236";
        private const string GroupExtraName2 = "M3301";
        private const string SomeoneRandom = "K-pop star";
        private const string SomeoneElseRandom = "unknown";
        private const string MyName = "Ovanes";
        private IsuExtraService _isuExtraService;
        
        
        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
        }

        [Test]
        public void AddElective()
        {
            Elective elective =_isuExtraService.AddElective(GroupExtraName1);
            Assert.AreEqual(GroupExtraName1[..2], elective.FacultyId);
        }

        [Test]
        public void AddPersonToElective()
        {
            Group group = _isuExtraService.AddGroup(GroupM3208);
            Person student = _isuExtraService.AddStudent(MyName, group);
            PersonExtra studentExtra = _isuExtraService.AddStudentExtra(student);
            Elective elective = _isuExtraService.AddElective("M32100");
            Group groupElective = _isuExtraService.AddGroup(GroupExtraName1);
            _isuExtraService.AddGroupToElective(groupElective, elective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra, groupElective);
            PersonExtra studentFromElective = _isuExtraService.GetStudentsFromElectiveByStream(elective).First();
            Assert.AreEqual(studentExtra, studentFromElective);
        }

        [Test]
        public void RemovePersonFromElective()
        {
            Group group = _isuExtraService.AddGroup(GroupM3208);
            Person student = _isuExtraService.AddStudent(MyName, group);
            PersonExtra studentExtra = _isuExtraService.AddStudentExtra(student);
            Elective elective = _isuExtraService.AddElective("M32100");
            Group groupElective = _isuExtraService.AddGroup(GroupExtraName1);
            _isuExtraService.AddGroupToElective(groupElective, elective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra, groupElective);
            _isuExtraService.RemoveStudentFromElective(studentExtra, elective);
            Assert.True(studentExtra.ElectivesGroup.Count == 0);
        }

        [Test]
        public void GetStreamByCourse()
        {
            Group group1 = _isuExtraService.AddGroup(GroupM3208);
            Group group2 = _isuExtraService.AddGroup(GroupM3310);
            Person student1 = _isuExtraService.AddStudent(MyName, group1);
            Person student2 = _isuExtraService.AddStudent(SomeoneRandom, group2);
            PersonExtra studentExtra1 = _isuExtraService.AddStudentExtra(student1);
            PersonExtra studentExtra2 = _isuExtraService.AddStudentExtra(student2);
            Elective elective = _isuExtraService.AddElective("M32100");
            Group groupElective = _isuExtraService.AddGroup(GroupExtraName1);
            _isuExtraService.AddGroupToElective(groupElective, elective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra1, groupElective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra2, groupElective);
            IEnumerable<PersonExtra> students = _isuExtraService.GetStudentsFromElectiveByStream(elective);
            Assert.AreEqual(2, students.Count());
        }

        [Test]
        public void GetStudentsByElectiveGroup()
        { 
            Group group1 = _isuExtraService.AddGroup(GroupM3208);
            Group group2 = _isuExtraService.AddGroup(GroupM3310);
            Person student1 = _isuExtraService.AddStudent(MyName, group1);
            Person student2 = _isuExtraService.AddStudent(SomeoneRandom, group2);
            PersonExtra studentExtra1 = _isuExtraService.AddStudentExtra(student1);
            PersonExtra studentExtra2 = _isuExtraService.AddStudentExtra(student2);
            Elective elective = _isuExtraService.AddElective("M32100");
            Group groupElective = _isuExtraService.AddGroup(GroupExtraName1);
            _isuExtraService.AddGroupToElective(groupElective, elective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra1, groupElective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra2, groupElective);
            IEnumerable<PersonExtra> studentsFromFinder = _isuExtraService.FindStudentsByElectiveByStreamByGroup(groupElective);
            var listOfStudents = new List<PersonExtra>() {studentExtra1, studentExtra2};
            Assert.AreEqual(listOfStudents, studentsFromFinder);
        }

        [Test]
        public void GetStudentsWithoutElective()
        {
            Group group1 = _isuExtraService.AddGroup(GroupM3208);
            Group group2 = _isuExtraService.AddGroup(GroupM3310);
            Person student1 = _isuExtraService.AddStudent(MyName, group1);
            Person student2 = _isuExtraService.AddStudent(SomeoneRandom, group2);
            PersonExtra studentExtra1 = _isuExtraService.AddStudentExtra(student1);
            PersonExtra studentExtra2 = _isuExtraService.AddStudentExtra(student2);
            Elective elective = _isuExtraService.AddElective("M32100");
            Elective elective1 = _isuExtraService.AddElective("M3153");
            Group groupElective = _isuExtraService.AddGroup(GroupExtraName1);
            Group groupElective1 = _isuExtraService.AddGroup(GroupExtraName2);
            _isuExtraService.AddGroupToElective(groupElective, elective);
            _isuExtraService.AddGroupToElective(groupElective1, elective1);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra1, groupElective);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra1, groupElective1);
            _isuExtraService.AddStudentToElectiveGroup(studentExtra2, groupElective);
            IEnumerable<PersonExtra> studentsFromFinder = _isuExtraService.GetStudentsWithoutElectives();
            Assert.AreEqual(studentExtra2, studentsFromFinder.First());
        }
    }
    
}