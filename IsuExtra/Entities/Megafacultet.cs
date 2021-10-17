using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Megafacultet
    {
        private List<OGNPCourse> _courses = new List<OGNPCourse>();
        private string _name;
        private char _acronym;

        public Megafacultet(string name, char acronym)
        {
            _name = name;
            _acronym = acronym;
        }

        public Megafacultet()
        {
        }

        public List<OGNPCourse> GetCoursesList()
        {
            return _courses;
        }

        public char GetAcronym()
        {
            return _acronym;
        }

        public void AddCourse(OGNPCourse course)
        {
            _courses.Add(course);
        }
    }
}