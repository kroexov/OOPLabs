using System;
using System.Collections.Generic;

namespace Banks.Entities
{
    public class Bank
    {
        private string _name;
        private List<Client> _clients = new List<Client>();
        private List<Account> _accounts = new List<Account>();
        private List<Client> _notifiedClients = new List<Client>();
        private double _percentage;
        private double _comission;
        private SortedDictionary<double, double> _floatcomissions;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public double Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;
            }
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

        public SortedDictionary<double, double> FloatComissions
        {
            get
            {
                return _floatcomissions;
            }
            set
            {
                _floatcomissions = value;
            }
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void UpdateTime(TimeSpan timeSpan)
        {
            foreach (var account in _accounts)
            {
                for (int i = 0; i < (int)timeSpan.Days; i++)
                {
                    account.SkipDay();
                    if (account.DaysGone == 30)
                    {
                        account.SkipMonth();
                    }
                }
            }
        }

        public void AddNotifiedClient(Client client)
        {
            _notifiedClients.Add(client);
        }

        public void RemoveNotifiedClient(Client client)
        {
            _notifiedClients.Remove(client);
        }

        private Bank SetName(string name)
        {
            _name = name;
            return this;
        }

        private Bank SetPercentage(double percentage)
        {
            _percentage = percentage;
            return this;
        }

        private Bank SetComission(double comission)
        {
            _comission = comission;
            return this;
        }

        private Bank SetFloatComissions(SortedDictionary<double, double> floatcomissions)
        {
            _floatcomissions = floatcomissions;
            return this;
        }

        public class Builder
        {
            private Bank _bank = new Bank();

            public Bank.Builder SetName(string name)
            {
                _bank.SetName(name);
                return this;
            }

            public Bank.Builder SetPercentage(double percentage)
            {
                _bank.SetPercentage(percentage);
                return this;
            }

            public Bank.Builder SetComission(double comission)
            {
                _bank.SetComission(comission);
                return this;
            }

            public Bank.Builder SetFloatComissions(SortedDictionary<double, double> floatcomissions)
            {
                _bank.SetFloatComissions(floatcomissions);
                return this;
            }

            public Bank Build()
            {
                return _bank;
            }
        }
    }
}