namespace Banks.Entities
{
    public abstract class Account
    {
        private string _id;
        private double _summ;
        private Client _client;
        private int _days = 0;

        public Account(string id, Client client)
        {
            _id = id;
            _client = client;
            _summ = 0;
        }

        public delegate void Notification(Account account, string notification);

        public event Notification Notify;
        public double Summ
        {
            get => _summ;
            set => _summ = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public int DaysGone
        {
            get => _days;
            set => _days = value;
        }

        public Client Client
        {
            get => _client;
        }

        public void WithdrawMoney(double summ)
        {
            _summ -= summ;
        }

        public void AddMoney(double summ)
        {
            _summ += summ;
        }

        public void SubscribeClient()
        {
            Notify += _client.Notification;
        }

        public void UnsubscribeClient()
        {
            Notify -= _client.Notification;
        }

        public virtual void SkipDay()
        {
        }

        public virtual void SkipMonth()
        {
        }

        protected virtual void OnNotify(Account account, string notification)
        {
            Notify?.Invoke(account, notification);
        }
    }
}