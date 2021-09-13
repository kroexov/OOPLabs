using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private const char First = 'M';
        private const char Second = '3';
        private static string _groupName;

        public GroupName(string groupName)
        {
            int groupnum = 0;
            string ending = groupName.Substring(2);
            bool j = int.TryParse(ending, out groupnum);
            if (groupName.Length == 5
                && groupName[0] == First
                && groupName[1] == Second
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