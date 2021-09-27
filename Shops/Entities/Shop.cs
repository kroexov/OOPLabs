using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private string _name;
        private List<Product> _products = new List<Product>();
        public Shop(string name)
        {
            _name = name;
        }

        public string Name()
        {
            return _name;
        }

        public void RegisterProduct(string name)
        {
            Product product = new Product(name);
            _products.Add(product);
        }

        public void AddProduct(string name, int price, int count)
        {
            foreach (var product in _products)
            {
                if (product.Name().Equals(name))
                {
                    product.SetCount(count);
                    product.SetPrice(price);
                    return;
                }
            }

            throw new ShopsException("product is not registered!");
        }

        public void Buy(Customer customer, string name, int count)
        {
            foreach (var product in _products)
            {
                if (product.Name() == name && customer.Money() >= product.Price() * count && product.Count() > count)
                {
                    customer.SetMoney(customer.Money() - (product.Price() * count));
                    product.SetCount(product.Count() - count);
                    return;
                }
            }

            throw new ShopsException("You can't buy this!");
        }

        public Product GetProduct(Product product)
        {
            foreach (var newproduct in _products)
            {
                if (product.Name() == newproduct.Name())
                {
                    return newproduct;
                }
            }

            return null;
        }

        public void SetPrice(Product product, int newprice)
        {
            foreach (var newproduct in _products)
            {
                if (product.Name() == newproduct.Name())
                {
                    newproduct.SetPrice(newprice);
                }
            }
        }
    }
}