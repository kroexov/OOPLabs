namespace Banks.Entities
{
    public class CreditAccount : Account
    {
        private double _comission;
        private double _minusSumm = 0;
        public CreditAccount(string id, Client client, double comission)
            : base(id, client)
        {
            _comission = comission;
        }

        public double Comission
        {
            get
            {
                return _comission;
            }
            set
            {
                _comission = value;
            }
        }

        public double MinusSumm
        {
            get
            {
                return _minusSumm;
            }
            set
            {
                _minusSumm = value;
            }
        }

        public override void SkipDay()
        {
            this.DaysGone++;
            _minusSumm += _comission;
        }

        public override void SkipMonth()
        {
            this.DaysGone = 0;
            this.Summ -= _minusSumm;
        }
    }
}