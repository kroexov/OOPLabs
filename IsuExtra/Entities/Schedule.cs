using System;
using System.Collections.Generic;
using System.IO;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private List<Pair> _pairs = new List<Pair>();

        public void AddPair(Pair newpair)
        {
            foreach (var pair in _pairs)
            {
                if (pair.Day.Equals(newpair.Day) && pair.Number.Equals(newpair.Number))
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
    }
}