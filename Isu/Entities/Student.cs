namespace Isu.Entities
{
    public class Student
    {
        private Group _group;
        private int _id;
        private string _name;

        public Student(Group group, string name)
        {
            _group = group;
            _name = name;
            _id = GetHashCode();
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetGroupName()
        {
            return _group.GetName();
        }
    }
}