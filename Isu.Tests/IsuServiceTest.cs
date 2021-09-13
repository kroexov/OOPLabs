using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuServise();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group zero = _isuService.AddGroup("M3200");
            Student A = _isuService.AddStudent(zero,"Alesha");
            Assert.AreEqual("M3200",A.GetGroupName());
            Assert.AreEqual("Alesha",zero.FindStudent("Alesha").GetName());
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group one = _isuService.AddGroup("M3201");
                for (int i = 0; i < 31; i++)
                {
                    _isuService.AddStudent(one,i.ToString());
                }
                _isuService.AddStudent(one,"32");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group two = _isuService.AddGroup("M3r22");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            
            Group zero = _isuService.AddGroup("M3200");
            Student A = _isuService.AddStudent(zero,"Alesha");
            Assert.AreEqual("M3200", A.GetGroupName());
            Group one = _isuService.AddGroup("M3201");
            _isuService.ChangeStudentGroup(A,one);
            Assert.AreEqual("M3201", A.GetGroupName());
        }
    }
}