using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxStudentsCount = 30;
        private GroupName _name;
        private List<Student> _students = new List<Student>();

        public Group(string name)
        {
            _name = new GroupName(name);
        }

        public string GetName()
        {
            return GroupName.GetName();
        }

        public int GetStudentsCount()
        {
            return _students.Count;
        }

        public List<Student> GetStudentsList()
        {
            return _students;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _students)
            {
                if (student.GetId() == id)
                {
                    return student;
                }
            }

            throw new IsuException("Can't find this id");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _students)
            {
                if (student.GetName() == name)
                {
                    return student;
                }
            }

            throw new IsuException("Can't find this name");
        }

        public void AddStudent(Student student)
        {
            ValidCount();
            _students.Add(student);
        }

        public void DeleteStudent(int id)
        {
            foreach (Student student in _students)
            {
                if (student.GetId() == id)
                {
                    _students.Remove(student);
                    return;
                }
            }

            throw new IsuException("Can't find this id");
        }

        private void ValidCount()
        {
            if (_students.Count >= MaxStudentsCount)
            {
                throw new IsuException("Reached maximum of students!");
            }

            return;
        }
    }
}