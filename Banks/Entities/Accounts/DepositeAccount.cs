using System;
using System.Collections.Generic;

namespace Banks.Entities
{
    public class DepositeAccount : Account
    {
        private SortedDictionary<double, double> _floatComissions;
        private double _extraSumm;
        public DepositeAccount(string id, Client client, SortedDictionary<double, double> floatComissions)
            : base(id, client)
        {
            _floatComissions = floatComissions;
        }

        public override void SkipDay()
        {
            this.DaysGone++;
            double percentage = 0;
            foreach (var comission in _floatComissions)
            {
                if (this.Summ > comission.Key)
                    percentage = comission.Value;
            }

            _extraSumm += this.Summ * percentage;
        }

        public override void SkipMonth()
        {
            this.DaysGone = 0;
            this.Summ += _extraSumm;
        }
    }
}