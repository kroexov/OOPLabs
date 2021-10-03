using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopsService : IShopsService
    {
        private List<Shop> _shops = new List<Shop>();
        private List<Product> _registeredProducts = new List<Product>();
        private int maxSumm = int.MaxValue;
        public Shop FindShop(string name)
        {
            foreach (var shop in _shops)
            {
                if (shop.GetName() == name)
                {
                    return shop;
                }
            }

            return null;
        }

        public Shop Create(string name)
        {
            var shop = new Shop(name);
            _shops.Add(shop);
            return shop;
        }

        public Product CreateProduct(string name)
        {
            var product = new Product(name);
            _registeredProducts.Add(product);
            foreach (var shop in _shops)
            {
                shop.RegisterProduct(name);
            }

            return product;
        }

        public Shop FindCheapestVariant(Product product, int count)
        {
            int minSumm = maxSumm;
            Shop bestShop = null;
            if (!_registeredProducts.Contains(product))
            {
                throw new ShopsException("this product doesn't exist!");
            }

            foreach (var shop in _shops)
            {
                if (shop.GetProduct(product).GetCount() >= count)
                {
                    if (shop.GetProduct(product).GetPrice() * count < minSumm)
                    {
                        minSumm = shop.GetProduct(product).GetPrice() * count;
                        bestShop = shop;
                    }
                }
            }

            if (minSumm == maxSumm)
            {
                throw new ShopsException("not enough product in any shop");
            }

            return bestShop;
        }
    }
}