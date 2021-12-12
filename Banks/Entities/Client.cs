using System.Collections.Generic;

namespace Banks.Entities
{
    public class Client
    {
        private ClientExtraData _extraData;
        private string _fullname;
        private List<Account> _accounts = new List<Account>();

        public Client()
        {
            _extraData = new ClientExtraData(null, null);
        }

        public Client(string fullname, ClientExtraData extraData)
        {
            _fullname = fullname;
            _extraData = extraData;
        }

        public string Name
        {
            get => _fullname;
        }

        public bool Issuspect => _extraData.IsSuspect;

        public void AddExtraData(string adress, string passport)
        {
            _extraData = new ClientExtraData(adress, passport);
        }

        internal void Notification(Account account, string notification)
        {
        }

        private Client SetName(string name)
        {
            _fullname = name;
            return this;
        }

        private Client SetExtraData(string adress, string passport)
        {
            _extraData = new ClientExtraData(adress, passport);
            return this;
        }

        public class Builder
        {
            private Client _client = new Client();

            public Client.Builder SetName(string name)
            {
                _client.SetName(name);
                return this;
            }

            public Client.Builder SetExtraData(string adress, string passport)
            {
                _client.SetExtraData(adress, passport);
                return this;
            }

            public Client Build()
            {
                return _client;
            }
        }
    }
}