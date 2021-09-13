using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;
namespace Isu.Services
{
    public class IsuServise : IIsuService
    {
        private List<Course> _courses = new List<Course>();
        private List<Group> _groups = new List<Group>();
        private List<Student> _students = new List<Student>();
        public Group AddGroup(string name)
        {
            Group group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group @group, string name)
        {
            Student student = new Student(@group, name);
            @group.AddStudent(student);
            _students.Add(student);
            return student;
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

            return null;
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

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetName() == groupName)
                {
                    return group.GetStudentsList();
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            foreach (Course course in _courses)
            {
                if (course.GetNumber() == courseNumber.GetNumber())
                {
                    List<Student> courseStudents = new List<Student>();
                    foreach (Group group in course.GetGroupsList())
                    {
                        foreach (Student student in group.GetStudentsList())
                        {
                            courseStudents.Add(student);
                        }
                    }

                    return courseStudents;
                }
            }

            return null;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetName() == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (Course course in _courses)
            {
                if (course.GetNumber() == courseNumber.GetNumber())
                {
                    return course.GetGroupsList();
                }
            }

            return null;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (_students.Contains(student))
            {
                foreach (Group group in _groups)
                {
                    if (student.GetGroupName() == group.GetName())
                    {
                        group.DeleteStudent(student.GetId());
                        newGroup.AddStudent(student);
                        return;
                    }
                }

                throw new IsuException("can't find this group");
            }
            else
            {
                throw new IsuException("can't find this student");
            }
        }
    }
}