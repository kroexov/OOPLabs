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
            int groupnum = 0;
            string ending = group.GetName().Substring(2);
            int.TryParse(ending, out groupnum);
            _id = (groupnum * 100) + group.GetStudentsCount();
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