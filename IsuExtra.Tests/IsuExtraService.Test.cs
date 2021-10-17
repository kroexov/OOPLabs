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
            Schedule personal = _isuExtraService.AddPersonalSchedule(Ilya);
            Schedule common = _isuExtraService.AddOGNPSchedule(First);
            Pair personal1 = _isuExtraService.AddPair(personal,2, 1, "Teacher1", 228);
            Pair personal2 = _isuExtraService.AddPair(personal,  3, 1, "Teacher1", 228);
            Pair common1 = _isuExtraService.AddPair(common, First, 1, 1, "Teacher2", 212);
            _isuExtraService.AddStudent(Ilya,First);
            Assert.Contains(common1,personal.GetPairs());
            _isuExtraService.RemoveStudent(Ilya,First);
            Assert.Contains(Ilya,_isuExtraService.NotInOGNP(zero));
        }
        
        [Test]

        public void TryToAddStudent_StudentHasSameMegafacultet_StreamIsFull_ThereIsCrossedPairs()
        {
            //setup
            Group zero = _isuService.AddGroup("M3200");
            Student Ilya = _isuService.AddStudent(zero,"Ilya");
            Megafacultet Biotech = _isuExtraService.AddMegafacultet("bioengineering", 'B');
            OGNPCourse Wine = _isuExtraService.AddCourse("Wine_techologies", Biotech);
            OGNPStream First = _isuExtraService.AddStream(1, 1, Wine);
            OGNPStream Second = _isuExtraService.AddStream(1, 1, Wine);
            Schedule IlyaPersonal = _isuExtraService.AddPersonalSchedule(Ilya);
            Schedule FirstStream = _isuExtraService.AddOGNPSchedule(First);
            Schedule SecondStream = _isuExtraService.AddOGNPSchedule(Second);
            Pair IlyaPersonal1 = _isuExtraService.AddPair(IlyaPersonal,2, 1, "Teacher1", 228);
            Pair FirstStream1 = _isuExtraService.AddPair(FirstStream, First, 1, 1, "Teacher2", 212);
            Pair SecondStream1 = _isuExtraService.AddPair(SecondStream, First, 5, 1, "Teacher2", 212);
            _isuExtraService.AddStudent(Ilya,Second);
            
            //Same Megafacultet
            Group one = _isuService.AddGroup("B3200");
            Student SameMegafacultet = _isuService.AddStudent(one, "Artem");
            Schedule ArtemPersonal = _isuExtraService.AddPersonalSchedule(Ilya);
            Pair ArtemPersonal1 = _isuExtraService.AddPair(ArtemPersonal,4, 1, "Teacher1", 228);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(SameMegafacultet,Second);
            });

            //Stream Is Full
            Student StreamIsFull = _isuService.AddStudent(zero, "Anton");
            Schedule AntonPersonal = _isuExtraService.AddPersonalSchedule(Ilya);
            Pair AntonPersonal1 = _isuExtraService.AddPair(AntonPersonal,4, 1, "Teacher1", 228);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(StreamIsFull,First);
            });
            
            // There Is Crossed Pairs
            Student ThereIsCrossedPairs = _isuService.AddStudent(zero, "Andrei");
            Schedule AndreiPersonal = _isuExtraService.AddPersonalSchedule(Ilya);
            Pair AndreiPersonal1 = _isuExtraService.AddPair(AndreiPersonal,1, 1, "Teacher1", 228);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudent(StreamIsFull,First);
            });
            
        }
    }
}
