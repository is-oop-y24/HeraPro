using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private readonly GroupName _groupM3208 = new GroupName("M3208");
        private readonly GroupName _groupM3210 = new GroupName("M3210");
        private const string GroupWithInvalidName = "M3513";
        private const string GroupWithTooShortName = "M330";
        private const string SomeoneRandom = "K-pop star";
        private const string MyName = "Ovanes";

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService(4);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup(_groupM3208);
            Student student = _isuService.AddStudent(group, MyName);
            Assert.AreEqual(_groupM3208, _isuService.FindStudent(MyName).Group);
            Assert.True(_isuService.FindGroup(_groupM3208).ListOfStudents.Contains(student));

        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup(_groupM3208);
                _isuService.AddStudent(group, MyName);
                for (char i = 'A'; i < 'z'; ++i)
                {
                    _isuService.AddStudent(_isuService.FindGroup(_groupM3208), i.ToString());
                }
            });
        }
        


        [TestCase(GroupWithInvalidName)]
        [TestCase(GroupWithTooShortName)]
        public void CreateGroupWithInvalidName_ThrowException(string groupName)
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup(new GroupName(groupName));
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            
                Group group = _isuService.AddGroup(_groupM3208);
                _isuService.AddStudent(group, MyName);
                Group group1 = _isuService.AddGroup(_groupM3210);
                _isuService.AddStudent(group1, SomeoneRandom);
                _isuService.ChangeStudentGroup(_isuService.FindStudent(MyName), _isuService.FindGroup(_groupM3210));

                Assert.AreEqual(2, _isuService.FindGroup(_groupM3210).ListOfStudents.Count);
                Assert.AreEqual(0, _isuService.FindGroup(_groupM3208).ListOfStudents.Count);
                Assert.AreEqual(_groupM3210, _isuService.FindStudent(MyName).Group);
            
        }
    }
}