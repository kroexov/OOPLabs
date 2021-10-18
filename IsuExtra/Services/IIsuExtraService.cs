using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;
namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Megafacultet AddMegafacultet(string name, char acronym);
        void AddStudent(Student student, OGNPStream stream);
        OGNPStream AddStream(int number, int capacity, OGNPCourse course);
        OGNPCourse AddCourse(string name, Megafacultet megafacultet);

        Schedule AddPersonalSchedule(Student student);
        Schedule AddOGNPSchedule(OGNPStream stream);
        Pair AddPair(Schedule schedule, OGNPStream stream, int number, int day, string teacher, string auditory);
        Pair AddPair(Schedule schedule, int number, int day, string teacher, string auditory);
        void RemoveStudent(Student student, OGNPStream stream);
        List<Student> NotInOGNP(Group group);
        Schedule GetPersonalSchedule(Student student);
    }
}