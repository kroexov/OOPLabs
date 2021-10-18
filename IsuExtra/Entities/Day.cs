using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Day
    {
        private int _dayNumber;
        private List<Pair> _pairs = new List<Pair>();

        public Day(int dayNumber)
        {
            _dayNumber = dayNumber;
        }

        public void AddPair(Pair newpair)
        {
            foreach (var pair in _pairs)
            {
                if (pair.OfWeekDay.Equals(newpair.OfWeekDay) && pair.Number.Equals(newpair.Number))
                {
                    throw new IsuExtraException("You have crossed pairs!!!");
                }
            }

            _pairs.Add(newpair);
        }

        public List<Pair> GetPairs()
        {
            return _pairs;
        }

        public int GetDayNumber()
        {
            return _dayNumber;
        }
    }
}