namespace Isu.Entities
{
    public class CourseNumber
    {
        private int _number;

        public CourseNumber(int number)
        {
            _number = number;
        }

        public int GetNumber()
        {
            return _number;
        }
    }
}