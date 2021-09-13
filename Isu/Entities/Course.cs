using System.Collections.Generic;
namespace Isu.Entities
{
    public class Course
    {
        private List<Group> _groups = new List<Group>();
        private CourseNumber _courseNumber;

        public Course(int number)
        {
            _courseNumber = new CourseNumber(number);
        }

        public List<Group> GetGroupsList()
        {
            return _groups;
        }

        public int GetNumber()
        {
            return _courseNumber.GetNumber();
        }
    }
}