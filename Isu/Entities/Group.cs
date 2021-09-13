using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private GroupName _name;
        private List<Student> _students = new List<Student>();
        private int _studentsCount;

        public Group(string name)
        {
            _name = new GroupName(name);
            _studentsCount = 0;
        }

        public string GetName()
        {
            return GroupName.GetName();
        }

        public int GetStudentsCount()
        {
            return _studentsCount;
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
            _studentsCount++;
            ValidCount();
            _students.Add(student);
        }

        public void DeleteStudent(int id)
        {
            foreach (Student student in _students)
            {
                if (student.GetId() == id)
                {
                    _studentsCount--;
                    ValidCount();
                    _students.Remove(student);
                    return;
                }
            }

            throw new IsuException("Can't find this id");
        }

        private void ValidCount()
        {
            if (_studentsCount > 30)
            {
                _studentsCount = 30;
                throw new IsuException("Reached maximum of students!");
            }

            if (_studentsCount < 0)
            {
                _studentsCount = 0;
                throw new IsuException("No one to delete!");
            }

            return;
        }
    }
}