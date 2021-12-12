using Banks.Tools;

namespace Banks.Entities
{
    public class DebetAccount : Account
    {
        private double _percentage;
        private double _extraSumm;
        public DebetAccount(string id, Client client, double percentage)
            : base(id, client)
        {
            _percentage = percentage;
        }

        public new void WithdrawMoney(double summ)
        {
            if (this.Summ >= summ)
            {
                this.Summ -= summ;
            }
            else
            {
                throw new BanksException("not enough money on account!");
            }
        }

        public override void SkipDay()
        {
            this.DaysGone++;
            _extraSumm += this.Summ * _percentage;
        }

        public override void SkipMonth()
        {
            this.DaysGone = 0;
            this.Summ += _extraSumm;
        }
    }
}