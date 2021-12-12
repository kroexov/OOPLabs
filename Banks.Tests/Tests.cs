using System;
using System.Collections.Generic;
using Banks;
using Banks.Entities;
using Banks.Services;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank _centralBank;
        private ConsoleService _consoleService;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
            _consoleService = new ConsoleService();
        }

        [Test]
        public void CreateNormalClient_CreateSuspectedClient_UnsuspectClient()
        {
            SortedDictionary<double, double> floatcomissions = new SortedDictionary<double, double>()
            {
                {0, 0},
                {10000, 0.01},
                {100000, 0.02}
            };
            Bank sber = _centralBank.RegisterBank("Sber", 0.03, 100, floatcomissions);
            Client Normal = _centralBank.RegisterClient("Ilya", "Itmo", "Somedata", sber);
            Client Sus = _centralBank.RegisterClient("Sus", sber);
            Assert.AreEqual(Normal.Issuspect, false);
            Assert.AreEqual(Sus.Issuspect, true);
            _centralBank.AddExtraData(Sus, "bu", "random");
            Assert.AreEqual(Sus.Issuspect, false);
        }

        [Test]
        public void CancelOperation()
        {
            SortedDictionary<double, double> floatcomissions = new SortedDictionary<double, double>()
            {
                {0, 0},
                {10000, 0.01},
                {100000, 0.02}
            };
            Bank sber = _centralBank.RegisterBank("Sber", 0.03, 100, floatcomissions);
            Client Ilya = _centralBank.RegisterClient("Ilya", "Itmo", "Somedata", sber);
            Account first = _centralBank.RegisterDebetAccount(sber, Ilya);
            Account second = _centralBank.RegisterDebetAccount(sber, Ilya);
            _centralBank.AddMoney(first, 100);
            Assert.AreEqual(first.Summ, 100);
            Assert.AreEqual(second.Summ, 0);
            string id = _centralBank.TransferMoney(first, second, 100);
            Assert.AreEqual(first.Summ, 0);
            Assert.AreEqual(second.Summ, 100);
            _centralBank.CancelOperation(id);
            Assert.AreEqual(first.Summ, 100);
            Assert.AreEqual(second.Summ, 0);
        }

        [Test]
        public void SpendOneMonth_CheckTime_CheckSumm()
        {
            SortedDictionary<double, double> floatcomissions = new SortedDictionary<double, double>()
            {
                {0, 0},
                {10000, 0.01},
                {100000, 0.02}
            };
            Bank sber = _centralBank.RegisterBank("Sber", 0.03, 100, floatcomissions);
            Client Ilya = _centralBank.RegisterClient("Ilya", "Itmo", "Somedata", sber);
            Account first = _centralBank.RegisterDebetAccount(sber, Ilya);
            _centralBank.AddMoney(first, 100000);
            _centralBank.SkipTime(TimeSpan.FromDays(29));
            Assert.AreEqual(first.DaysGone, 29);
            _centralBank.SkipTime(TimeSpan.FromDays(1));
            Assert.Greater(first.Summ, 100000);
        }

        [Test]
        public void FloatComissions_Check()
        {
            SortedDictionary<double, double> floatcomissions = new SortedDictionary<double, double>()
            {
                {0, 0},
                {10000, 0.01},
                {100000, 0.02}
            };
            Bank sber = _centralBank.RegisterBank("Sber", 0.03, 100, floatcomissions);
            Client Ilya = _centralBank.RegisterClient("Ilya", "Itmo", "Somedata", sber);
            Account first = _centralBank.RegisterDepositeAccount(sber, Ilya);
            Account second = _centralBank.RegisterDepositeAccount(sber, Ilya);
            _centralBank.AddMoney(first, 100000);
            _centralBank.AddMoney(second, 110000);
            _centralBank.SkipTime(TimeSpan.FromDays(30));
            Assert.AreEqual(first.Summ, 130000);
            Assert.AreEqual(second.Summ, 176000);
        }
    }
}