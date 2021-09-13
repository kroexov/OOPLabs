using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private static string _groupName;

        public GroupName(string groupName)
        {
            int groupnum = 0;
            const char first = 'M';
            const char second = '3';
            string ending = groupName.Substring(2);
            bool j = int.TryParse(ending, out groupnum);
            if (groupName.Length == 5
                && groupName[0] == first
                && groupName[1] == second
                && j)
            {
                _groupName = groupName;
                return;
            }

            throw new IsuException("incorrect groupName");
        }

        public static string GetName()
        {
            return _groupName;
        }
    }
}