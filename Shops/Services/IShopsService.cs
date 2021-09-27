using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public interface IShopsService
    {
        Shop FindShop(string name);
        public Shop Create(string name);
        public Product CreateProduct(string name);

        public Shop FindCheapestVariant(Product product, int count);
    }
}