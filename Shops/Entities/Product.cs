namespace Shops.Entities
{
    public class Product
    {
        private int _price;
        private string _name;
        private int _count;

        public Product(string name)
        {
            _name = name;
        }

        public Product(string name, int price, int count)
        {
            _name = name;
            _price = price;
            _count = count;
        }

        public int Price()
        {
            return _price;
        }

        public int Count()
        {
            return _count;
        }

        public string Name()
        {
            return _name;
        }

        public void SetPrice(int price)
        {
            _price = price;
        }

        public void SetCount(int count)
        {
            _count = count;
        }
    }
}