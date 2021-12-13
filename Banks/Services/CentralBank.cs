using System;
using System.Collections.Generic;
using System.IO;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank
    {
        private List<Bank> _banks = new List<Bank>();
        private List<Account> _accounts = new List<Account>();
        private ConsoleService _consoleService = new ConsoleService();
        private List<Operation> _operations = new List<Operation>();
        private double _maxSuspectedSumm = 0;
        private DateTime _time = default;

        public double MaxSuspectedSumm
        {
            get => _maxSuspectedSumm;
            set => _maxSuspectedSumm = value;
        }

        public List<Account> Accounts
        {
            get => _accounts;
        }

        public List<Bank> Banks
        {
            get => _banks;
        }

        public Bank FindBank(string name)
        {
            foreach (var bank in _banks)
            {
                if (bank.Name.Equals(name))
                {
                    return bank;
                }
            }

            throw new BanksException("didn't find this bank!");
        }

        public Bank RegisterBank(string name, double percentage, double comission, SortedDictionary<double, double> floatcomissions)
        {
            foreach (var oldbank in _banks)
            {
                if (oldbank.Name.Equals(name))
                {
                    throw new BanksException("this name already exists!");
                }
            }

            var bankBuilder = new Bank.Builder();
            bankBuilder
                .SetName(name)
                .SetComission(comission)
                .SetPercentage(percentage)
                .SetFloatComissions(floatcomissions);
            var bank = bankBuilder.Build();
            _banks.Add(bank);
            return bank;
        }

        public void Start()
        {
            List<string> options = new List<string>() { "Client", "Bank", "Exit" };
            var who = _consoleService.OptionsAsking("Choose the type of start", options);
            switch (who)
            {
                case "Client":
                    ClientStart();
                    break;
                case "Bank":
                    BankStart();
                    break;
                case "Exit":
                    return;
            }
        }

        public void ClientStart()
        {
            _consoleService.ClientStart(this);
        }

        public void BankStart()
        {
            _consoleService.BankStart(this);
        }

        public void SkipTime(TimeSpan timeSpan)
        {
            _time += timeSpan;
            UpdateAllBanksTime(timeSpan);
        }

        public void UpdateAllBanksTime(TimeSpan timeSpan)
        {
            foreach (var bank in _banks)
            {
                bank.UpdateTime(timeSpan);
            }
        }

        public Account RegisterCreditAccount(Bank bank, Client client)
        {
            string id = _accounts.Count.ToString();
            Account creditAccount = new CreditAccount(id, client, bank.Comission);
            bank.AddAccount(creditAccount);
            client.AddAccount(creditAccount);
            _accounts.Add(creditAccount);
            return creditAccount;
        }

        public Account RegisterDebetAccount(Bank bank, Client client)
        {
            string id = _accounts.Count.ToString();
            Account debetAccount = new DebetAccount(id, client, bank.Percentage);
            bank.AddAccount(debetAccount);
            client.AddAccount(debetAccount);
            _accounts.Add(debetAccount);
            return debetAccount;
        }

        public Account RegisterDepositeAccount(Bank bank, Client client)
        {
            string id = _accounts.Count.ToString();
            Account depositeAccount = new DepositeAccount(id, client, bank.FloatComissions);
            bank.AddAccount(depositeAccount);
            client.AddAccount(depositeAccount);
            _accounts.Add(depositeAccount);
            return depositeAccount;
        }

        public Client RegisterClient(string name, Bank bank)
        {
            var clientBuilder = new Client.Builder();
            clientBuilder
                .SetName(name);
            Client client = clientBuilder.Build();
            bank.AddClient(client);
            return client;
        }

        public Client RegisterClient(string name, string adress, string passport, Bank bank)
        {
            var clientBuilder = new Client.Builder();
            clientBuilder
                .SetName(name)
                .SetExtraData(adress, passport);
            Client client = clientBuilder.Build();
            bank.AddClient(client);
            return client;
        }

        public void UnsuspectClient(Client client)
        {
            string adress = _consoleService.AskData("What is your address?");
            string passport = _consoleService.AskData("What is your passport data?");
            client.AddExtraData(adress, passport);
        }

        public void AddExtraData(Client client, string adress, string passport)
        {
            client.AddExtraData(adress, passport);
        }

        public string AddMoney(Account account, double summ)
        {
            account.AddMoney(summ);
            Operation newoperation = new Operation(_operations.Count.ToString(), summ, null, account);
            _consoleService.WriteMessage("adding " + summ + " to " + account.Client.Name);
            _operations.Add(newoperation);
            return newoperation.ID;
        }

        public string WithdrawMoney(Account account, double summ)
        {
            account.WithdrawMoney(summ);
            if (account.Client.Issuspect && summ > _maxSuspectedSumm)
            {
                throw new BanksException("Account is suspected and summ is too big!");
            }

            Operation newoperation = new Operation(_operations.Count.ToString(), summ, account, null);
            _consoleService.WriteMessage("withdrawing " + summ + " from " + account.Client.Name);
            _operations.Add(newoperation);
            return newoperation.ID;
        }

        public string TransferMoney(Account from, Account to, double summ)
        {
            from.WithdrawMoney(summ);

            to.AddMoney(summ);
            _consoleService.WriteMessage("transfering " + summ + " from " + from.Client.Name + " to " + to.Client.Name);
            Operation newoperation = new Operation(_operations.Count.ToString(), summ, from, to);
            _operations.Add(newoperation);
            return newoperation.ID;
        }

        public void CancelOperation(string id)
        {
            foreach (var operation in _operations)
            {
                if (operation.ID.Equals(id))
                {
                    if (operation.IsCancelled.Equals(true))
                    {
                        throw new BanksException("This operation is already cancelled!");
                    }
                    else
                    {
                        _consoleService.WriteMessage("cancelling operation " + id);
                        operation.IsCancelled = true;
                        operation.Sender.AddMoney(operation.Summ);
                        operation.Reciever.WithdrawMoney(operation.Summ);
                        return;
                    }
                }
            }
        }
    }
}