using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private static string _groupName;

        public GroupName(string groupName)
        {
            int groupnum = 0;
            string ending = groupName.Substring(1);
            bool isgoodname = int.TryParse(ending, out groupnum);
            if (groupName.Length == 5
                && isgoodname)
            {
                _groupName = groupName;
                return;
            }

            throw new IsuException("incorrect group name");
        }

        public static string GetName()
        {
            return _groupName;
        }
    }
}