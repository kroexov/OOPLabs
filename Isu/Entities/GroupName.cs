using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private static string _groupName;

        public GroupName(string groupName)
        {
            const char first = 'M';
            const char second = '3';
            string ending = groupName.Substring(2);
            bool.TryParse(ending, out bool j);
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