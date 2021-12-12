using System.Collections.Generic;
using Banks.Services;

namespace Banks.Entities
{
    public class ClientBuilder : IClientBuilder
    {
        private string _name;
        private string _address;
        private string _passport;

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetAddress(string address)
        {
            _address = address;
        }

        public void SetPassport(string passport)
        {
            _passport = passport;
        }

        public Client GetResult()
        {
            ClientExtraData extraData = new ClientExtraData(_address, _passport);
            return new Client(_name, extraData);
        }
    }
}