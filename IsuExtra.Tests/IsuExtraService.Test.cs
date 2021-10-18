using System;
using System.Runtime.ConstrainedExecution;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtraService;
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _isuService = new IsuServise();
        }

        [Test]

        public void TryToAddAndRemoveStudent_StudentRemoved_ScheduleChanged_StudentIsNotInOGNP()
        {
            Group zero = _isuService.AddGroup("M3200");
            Student Ilya = _isuService.AddStudent(zero,"Ilya");
            Megafacultet Biotech = _isuExtraService.AddMegafacultet("bioengineering", 'B');
            OGNPCourse Wine = _isuExtraService.AddCourse("Wine_techologies", Biotech);
            OGNPStream First = _isuExtraService.AddStream(1, 30, Wine);
            Schedule group = _isuExtraService.AddGroupSchedule("M3200");
            Schedule common = _isuExtraService.AddOGNPSchedule(First);
            Pair group1 = _isuExtraService.AddPair(group, 1, 2, DayOfWeek.Monday, "Teacher1", "228");
            Pair group2 = _isuExtraService.AddPair(group, 1, 3, DayOfWeek.Monday, "Teacher1", "228");
            Pair common1 = _isuExtraService.AddPair(common, 1, 1, DayOfWeek.Monday, "Teacher2", "212");
            _isuExtraService.AddStudent(Ilya,First);
            Assert.Contains(Ilya,First.GetStudentsList());
            _isuExtraService.RemoveStudent(Ilya,First);
            Assert.Contains(Ilya,_isuExtraService.NotInOGNP(zero));
        }
        
        [Test]

        public void TryToAddStudent_StudentHasSameMegafacultet_StreamIsFull_ThereIsCrossedPairs()
        {
            //setup
            Group zero = _isuService.AddGroup("M3200");
            Student Ilya = _isuService.AddStudent(zero,"Ilya");
            Megafacultet Biotech = _isuExtraService.AddMegafacultet("Bioengineering", 'B');
            OGNPCourse Wine = _isuExtraService.AddCourse("Wine_techologies", Biotech);
            OGNPStream First = _isuExtraService.AddStream(1, 1, Wine);
            OGNPStream Second = _isuExtraService.AddStream(1, 1, Wine);
            Schedule M3200 = _isuExtraService.AddGroupSchedule("M3200");
            Schedule FirstStream = _isuExtraService.AddOGNPSchedule(First);
            Schedule SecondStream = _isuExtraService.AddOGNPSchedule(Second);
            Pair M32001 = _isuExtraService.AddPair(M3200, 1, 2, DayOfWeek.Monday, "Teacher1", "228");
            Pair FirstStream1 = _isuExtraService.AddPair(FirstStream, 1, 1, DayOfWeek.Monday, "Teacher2", "212");
            Pair SecondStream1 = _isuExtraService.AddPair(SecondStream, 1, 5, DayOfWeek.Monday, "Teacher2", "212");
            _isuExtraService.AddStudent(Ilya,First);
            
            //Stream Is Full
            Student StreamIsFull = _isuService.AddStudent(zero, "Anton");
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(StreamIsFull,First);
            });
            
            //Same Megafacultet
            Group one = _isuService.AddGroup("B3200");
            Schedule B3200 = _isuExtraService.AddGroupSchedule("B3200");
            Pair B32001 = _isuExtraService.AddPair(B3200, 1, 3, DayOfWeek.Monday, "Teacher1", "228");
            Student SameMegafacultet = _isuService.AddStudent(one, "Artem");
            Console.WriteLine(Biotech.GetAcronym());
            Console.WriteLine(SameMegafacultet.GetGroupName());
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(SameMegafacultet,Second);
            });

            // There Is Crossed Pairs
            Group two = _isuService.AddGroup("M3201");
            Schedule M3201 = _isuExtraService.AddGroupSchedule("M3201");
            Pair M32011 = _isuExtraService.AddPair(M3201, 1, 5, DayOfWeek.Monday, "Teacher1", "228");
            Student ThereIsCrossedPairs = _isuService.AddStudent(two, "Andrei");
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(ThereIsCrossedPairs,Second);
            });
            
        }
    }
}
