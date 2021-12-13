using System;
using System.Collections.Generic;
using Banks.Entities;
using Spectre.Console;

namespace Banks.Services
{
    public class ConsoleService
    {
        public string AskData(string message)
        {
            string returnData = AnsiConsole.Ask<string>("[green]" + message + "[/]");
            return returnData;
        }

        public void WriteMessage(string message)
        {
            AnsiConsole.Markup("[green]" + message + "[/]" + "\n");
        }

        public string OptionsAsking(string message, List<string> options)
        {
            var answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[red]" + message + "[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(options));
            return answer;
        }

        public void ClientStart(CentralBank centralBank)
        {
            List<string> options = new List<string>()
                { "RegisterMe", "Create new account", "Make operation with existing account", "AddExtraData", "Exit" };
            var option = OptionsAsking("Choose operation", options);
            switch (option)
            {
                case "RegisterMe":
                    ClientRegistration(centralBank);
                    break;
                case "Create new account":
                    AccountCreation(centralBank);
                    break;
                case "Make operation with existing account":
                    Operation(centralBank);
                    break;
                case "AddExtraData":
                    ExtraClientData(centralBank);
                    break;
                case "Exit":
                    return;
            }
        }

        public void BankStart(CentralBank centralBank)
        {
            List<string> options = new List<string>()
                { "RegisterMe", "Change global terms and conditions", "Cancel operation", "Exit" };
            var option = OptionsAsking("Choose operation", options);
            switch (option)
            {
                case "RegisterMe":
                    BankRegistration(centralBank);
                    break;
                case "Change global terms and conditions":
                    ChangeTerms(centralBank);
                    break;
                case "Cancel operation":
                    CancelOperation(centralBank);
                    break;
                case "Exit":
                    return;
            }
        }

        public void ExtraClientData(CentralBank centralBank)
        {
            string bankName = AskData("What is your bank name?");
            Bank myBank = centralBank.FindBank(bankName);
            string clientName = AskData("What is your client name?");
            Client myClient = myBank.FindClient(clientName);
            string adress = AskData("What is your address?");
            string passport = AskData("What is your passport data?");
            centralBank.AddExtraData(myClient, adress, passport);
            centralBank.ClientStart();
        }

        public void Operation(CentralBank centralBank)
        {
            string bankName = AskData("What is your bank name?");
            Bank myBank = centralBank.FindBank(bankName);
            string clientName = AskData("What is your client name?");
            Client myClient = myBank.FindClient(clientName);
            List<string> options = new List<string>() { "Add money", "Withdraw money", "Transfer money" };
            var option = OptionsAsking("Choose operation type", options);
            List<string> accountIDs = new List<string>();
            foreach (var account in myClient.Accounts)
            {
                accountIDs.Add(account.Id);
            }

            var id = OptionsAsking("Choose account", accountIDs);
            Account myAccount = myClient.FindAccount(id);
            switch (option)
            {
                case "Add money":
                    string addSumm = AskData("What summ do you want to add?");
                    centralBank.AddMoney(myAccount, Convert.ToDouble(addSumm));
                    break;
                case "Withdraw money":
                    string withdrawSumm = AskData("What summ do you want to withdraw?");
                    centralBank.AddMoney(myAccount, Convert.ToDouble(withdrawSumm));
                    break;
                case "Transfer money":
                    string summ = AskData("What summ do you want to add?");
                    string toid = AskData("Type id of the account to send money");
                    foreach (var account in centralBank.Accounts)
                    {
                        if (account.Id.Equals(toid))
                        {
                            centralBank.TransferMoney(myAccount, account, Convert.ToDouble(summ));
                            break;
                        }
                    }

                    break;
            }

            ClientStart(centralBank);
        }

        public void AccountCreation(CentralBank centralBank)
        {
            string bankName = AskData("What is your bank name?");
            Bank myBank = centralBank.FindBank(bankName);
            string clientName = AskData("What is your client name?");
            Client myClient = myBank.FindClient(clientName);
            List<string> options = new List<string>() { "Credit", "Debet", "Deposite" };
            var option = OptionsAsking("Choose account type", options);
            switch (option)
            {
                case "Credit":
                    centralBank.RegisterCreditAccount(myBank, myClient);
                    break;
                case "Debet":
                    centralBank.RegisterDebetAccount(myBank, myClient);
                    break;
                case "Deposite":
                    centralBank.RegisterDepositeAccount(myBank, myClient);
                    break;
            }

            ClientStart(centralBank);
        }

        public void ClientRegistration(CentralBank centralBank)
        {
            List<string> banks = new List<string>();
            foreach (var bank in centralBank.Banks)
            {
                banks.Add(bank.Name);
            }

            string name = AskData("What is your name?");
            List<string> options = new List<string>() { "Yes", "No" };
            var adddata = OptionsAsking("Are you ready to share your personal information?", options);
            var bankdata = OptionsAsking("Choose bank", banks);
            Bank mybank = centralBank.FindBank(bankdata);
            if (adddata.Equals("Yes"))
            {
                string adress = AskData("What is your address?");
                string passport = AskData("What is your passport data?");
                Client client = centralBank.RegisterClient(name, adress, passport, mybank);
                mybank.AddClient(client);
            }
            else
            {
                Client client = centralBank.RegisterClient(name, mybank);
                mybank.AddClient(client);
            }

            ClientStart(centralBank);
        }

        public void BankRegistration(CentralBank centralBank)
        {
            string name = AskData("What is your bank name?");
            string percentage = AskData("What is your bank percentage?");
            string comission = AskData("What is your bank comission?");
            Bank bank = centralBank.RegisterBank(name, Convert.ToDouble(percentage), Convert.ToDouble(comission), new SortedDictionary<double, double>());
            BankStart(centralBank);
        }

        public void ChangeTerms(CentralBank centralBank)
        {
            BankStart(centralBank);
        }

        public void CancelOperation(CentralBank centralBank)
        {
            string id = AskData("What is the operation id?");
            centralBank.CancelOperation(id);
            BankStart(centralBank);
        }
    }
}