using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<Student> _allOGNPStudents = new List<Student>();
        private List<Megafacultet> _megafacultets = new List<Megafacultet>();
        private List<(Student, Schedule)> _personalSchedules = new List<(Student, Schedule)>();

        public Megafacultet AddMegafacultet(string name, char acronym)
        {
            Megafacultet megafacultet = new Megafacultet(name, acronym);
            _megafacultets.Add(megafacultet);
            return megafacultet;
        }

        public void AddStudent(Student student, OGNPStream stream)
        {
            if (student.GetGroupName()[0].Equals(stream.GetOGNPAcronym()))
            {
                throw new IsuExtraException("This student tries to choose his own megafacultet!");
            }

            Schedule personalSchedule = GetPersonalSchedule(student);
            foreach (var pair in stream.GetSchedule().GetPairs())
            {
                personalSchedule.AddPair(pair);
            }

            _allOGNPStudents.Add(student);
            stream.AddStudent(student);
        }

        public OGNPStream AddStream(int number, int capacity, OGNPCourse course)
        {
            OGNPStream stream = new OGNPStream(course, number, capacity);
            course.AddStream(stream);
            return stream;
        }

        public OGNPCourse AddCourse(string name, Megafacultet megafacultet)
        {
            OGNPCourse course = new OGNPCourse(name, megafacultet);
            megafacultet.AddCourse(course);
            return course;
        }

        public Schedule AddPersonalSchedule(Student student)
        {
            Schedule newSchedule = new Schedule();
            _personalSchedules.Add((student, newSchedule));
            return newSchedule;
        }

        public Schedule AddOGNPSchedule(OGNPStream stream)
        {
            Schedule newSchedule = new Schedule();
            stream.SetSchedule(newSchedule);
            return newSchedule;
        }

        public Pair AddPair(Schedule schedule, OGNPStream stream, int number, int day, string teacher, int auditory)
        {
            Pair pair = new Pair(stream, number, day, teacher, auditory);
            schedule.AddPair(pair);
            return pair;
        }

        public Pair AddPair(Schedule schedule,  int number, int day, string teacher, int auditory)
        {
            Pair pair = new Pair(number, day, teacher, auditory);
            schedule.AddPair(pair);
            return pair;
        }

        public void RemoveStudent(Student student, OGNPStream stream)
        {
            if (!stream.GetStudentsList().Contains(student))
            {
                throw new IsuExtraException("This student isn't found in stream!");
            }

            if (!_allOGNPStudents.Contains(student))
            {
                throw new IsuExtraException("This student isn't found in system!");
            }

            Schedule personalSchedule = GetPersonalSchedule(student);
            Schedule newSchedule = new Schedule();
            foreach (var pair in personalSchedule.GetPairs())
            {
                if (!pair.Stream.Equals(stream))
                {
                    newSchedule.AddPair(pair);
                }
            }

            SetPersonalSchedule(student, newSchedule);

            stream.GetStudentsList().Remove(student);
            _allOGNPStudents.Remove(student);
        }

        public List<Student> NotInOGNP(Group group)
        {
            List<Student> nonOGNP = new List<Student>();
            foreach (var student in group.GetStudentsList())
            {
                if (!_allOGNPStudents.Contains(student))
                {
                    nonOGNP.Add(student);
                }
            }

            return nonOGNP;
        }

        public Schedule GetPersonalSchedule(Student student)
        {
            foreach (var personalSchedule in _personalSchedules)
            {
                if (personalSchedule.Item1.Equals(student))
                {
                    return personalSchedule.Item2;
                }
            }

            throw new IsuExtraException("Didn't find this student!");
        }

        public void SetPersonalSchedule(Student student, Schedule schedule)
        {
            foreach (var personalSchedule in _personalSchedules)
            {
                if (personalSchedule.Item1.Equals(student))
                {
                    _personalSchedules.Remove(personalSchedule);
                    _personalSchedules.Add((student, schedule));
                    return;
                }
            }

            throw new IsuExtraException("Didn't find this student!");
        }
    }
}