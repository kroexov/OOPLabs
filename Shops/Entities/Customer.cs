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

        public int GetMoney()
        {
            return _money;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetMoney(int summ)
        {
            _money = summ;
        }
    }
}