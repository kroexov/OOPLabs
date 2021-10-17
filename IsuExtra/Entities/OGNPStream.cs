using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class OGNPStream
    {
        private OGNPCourse _course;
        private int _number;
        private int _capacity = 0;
        private Schedule _schedule;
        private List<Student> _students = new List<Student>();

        public OGNPStream(OGNPCourse course, int number, int capacity)
        {
            _course = course;
            _number = number;
            _capacity = capacity;
        }

        public OGNPStream()
        {
            _course = new OGNPCourse();
        }

        public char GetOGNPAcronym()
        {
            return _course.GetMegafacultetAcronym();
        }

        public void SetCapacity(int capacity)
        {
            _capacity = capacity;
        }

        public void SetSchedule(Schedule schedule)
        {
            _schedule = schedule;
        }

        public int GetCapacity()
        {
            return _capacity;
        }

        public Schedule GetSchedule()
        {
            return _schedule;
        }

        public List<Student> GetStudentsList()
        {
            return _students;
        }

        public void AddStudent(Student student)
        {
            if (_students.Count < _capacity)
            {
                _students.Add(student);
            }
            else
            {
                throw new IsuExtraException("Too many students!");
            }
        }
    }
}