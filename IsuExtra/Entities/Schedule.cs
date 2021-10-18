using System;
using System.Collections.Generic;
using System.IO;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private List<Day> _days = new List<Day>();

        public void AddPair(Pair newPair, int day)
        {
            foreach (var curday in _days)
            {
                if (curday.GetDayNumber().Equals(day))
                {
                    curday.AddPair(newPair);
                    return;
                }
            }

            Day newday = new Day(day);
            newday.AddPair(newPair);
            _days.Add(newday);
        }

        public void AddDay(Day newDay)
        {
            _days.Add(newDay);
        }

        public List<Day> GetDays()
        {
            return _days;
        }
    }
}