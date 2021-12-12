using System.Collections.Generic;

namespace Banks.Entities
{
    public class Operation
    {
        private string _id;
        private bool _isCancelled = false;
        private double _summ;
        private Account _sender;
        private Account _reciever;

        public Operation(string id, double summ, Account sender, Account reciever)
        {
            _id = id;
            _summ = summ;
            _sender = sender;
            _reciever = reciever;
        }

        public double Summ
        {
            get => _summ;
            set => _summ = value;
        }

        public string ID
        {
            get => _id;
            set => _id = value;
        }

        public bool IsCancelled
        {
            get => _isCancelled;
            set => _isCancelled = value;
        }

        public Account Sender
        {
            get => _sender;
        }

        public Account Reciever
        {
            get => _reciever;
        }
    }
}