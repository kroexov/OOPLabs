using System;
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
        private Dictionary<string, Schedule> _groupSchedules = new Dictionary<string, Schedule>();

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

            Schedule personalSchedule = new Schedule();
            foreach (var groupSchedule in _groupSchedules)
            {
                if (groupSchedule.Key.Equals(student.GetGroupName()))
                {
                    personalSchedule = groupSchedule.Value;
                    break;
                }
            }

            foreach (var day in stream.GetSchedule().GetDays())
            {
                foreach (var personalDay in personalSchedule.GetDays())
                {
                    if (day.GetDayNumber().Equals(personalDay.GetDayNumber()))
                    {
                        foreach (var pair in day.GetPairs())
                        {
                            foreach (var personalPair in personalDay.GetPairs())
                            {
                                if (pair.Number.Equals(personalPair.Number))
                                {
                                    throw new IsuExtraException("Student has crossing pairs!");
                                }
                            }
                        }
                    }
                }
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

        public Schedule AddGroupSchedule(string group)
        {
            Schedule newSchedule = new Schedule();
            _groupSchedules.Add(group, newSchedule);
            return newSchedule;
        }

        public Schedule AddOGNPSchedule(OGNPStream stream)
        {
            Schedule newSchedule = new Schedule();
            stream.SetSchedule(newSchedule);
            return newSchedule;
        }

        public Pair AddPair(Schedule schedule, OGNPStream stream, int day, int number, DayOfWeek dayOfWeek, string teacher, string auditory)
        {
            Pair pair = new Pair(stream, number, dayOfWeek, teacher, auditory);
            schedule.AddPair(pair, day);
            return pair;
        }

        public Pair AddPair(Schedule schedule, int day, int number, DayOfWeek dayOfWeek, string teacher, string auditory)
        {
            Pair pair = new Pair(number, dayOfWeek, teacher, auditory);
            schedule.AddPair(pair, day);
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

        public Schedule GetGroupSchedule(string group)
        {
            foreach (var groupSchedule in _groupSchedules)
            {
                if (groupSchedule.Key.Equals(group))
                {
                    return groupSchedule.Value;
                }
            }

            throw new IsuExtraException("Didn't find this group!");
        }

        public void SetGroupSchedule(string group, Schedule schedule)
        {
            foreach (var groupSchedule in _groupSchedules)
            {
                if (groupSchedule.Key.Equals(group))
                {
                    _groupSchedules.Remove(group);
                    _groupSchedules.Add(group, schedule);
                    return;
                }
            }

            throw new IsuExtraException("Didn't find this group!");
        }
    }
}