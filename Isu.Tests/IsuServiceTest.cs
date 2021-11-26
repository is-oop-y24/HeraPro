using System;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private const string GroupM3208 = "M3208";
        private const string GroupM3210 = "M3210";
        private const string GroupWithInvalidName = "M35136";
        private const string GroupWithTooShortName = "M330";
        private const string SomeoneRandom = "K-pop star";
        private const string MyName = "Ovanes";

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup(GroupM3208);
            Person person = _isuService.AddStudent(MyName, group);
            Person personFromGroup = _isuService.GetStudentsByGroups(group).First();
            Assert.AreEqual(person, personFromGroup);
            Assert.AreEqual(group, _isuService.FindPersonByName(MyName).Group);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup(GroupM3208);
                _isuService.AddStudent(MyName, group);
                for (char i = 'A'; i < 'z'; ++i)
                {
                    _isuService.AddStudent(i.ToString(), group);
                }
            });
        }
        


        [TestCase(GroupWithInvalidName)]
        [TestCase(GroupWithTooShortName)]
        public void CreateGroupWithInvalidName_ThrowException(string groupName)
        {
            Assert.Catch<IsuException>(() =>
            {

                _isuService.AddGroup(groupName);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            
                Group group = _isuService.AddGroup(GroupM3208);
                _isuService.AddStudent(MyName, group);
                _isuService.AddStudent(SomeoneRandom, group);
                Group group1 = _isuService.AddGroup(GroupM3210);
                _isuService.AddStudent(SomeoneRandom, group1);
                Person person = _isuService.ChangeStudentGroup(_isuService.FindPersonByName(MyName), _isuService.FindGroup(GroupM3210));
                
                Assert.AreEqual(2, _isuService.GetStudentsByGroups(group1).Count());
                Assert.AreEqual(1, _isuService.GetStudentsByGroups(group).Count());
                Assert.AreEqual(GroupM3210, person.Group.GroupName.Name);
                
        }
    }
}