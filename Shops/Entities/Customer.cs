namespace Shops.Entities
{
    public class Customer
    {
        private string _name;
        private int _money;
        public Customer(string name, int money)
        {
            _name = name;
            _money = money;
        }

        public int Money()
        {
            return _money;
        }

        public string Name()
        {
            return _name;
        }

        public void SetMoney(int summ)
        {
            _money = summ;
        }
    }
}