using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopsService _shopsService;
        
        [SetUp]
        public void Setup()
        {
            _shopsService = new ShopsService();
        }
        

        [Test]
        public void AddProductsTest()
        {
            int productPrice = 120;
            int productCount = 20;
            string productName = "sugar";
            var customer = new Customer("Ilya", 1000);
            var shop = _shopsService.Create("Magnit");
            var product = _shopsService.CreateProduct(productName);

            shop.AddProduct(productName, productPrice, productCount);
            shop.Buy(customer,productName,1);
            Assert.AreEqual(shop,_shopsService.FindShop("Magnit"));
            Assert.AreEqual(product.Name(),shop.GetProduct(product).Name());
        }
        
        [Test]
        public void SetAndChangePriceTest()
        {
            int OldPrice = 1;
            int NewPrice = 2;
            string productName = "sugar";
            var shop = _shopsService.Create("Magnit");
            var product = _shopsService.CreateProduct(productName);
            
            shop.AddProduct(productName, OldPrice, 1);
            Assert.AreEqual(shop.GetProduct(product).Price(),OldPrice);
            shop.SetPrice(product, NewPrice);
            Assert.AreEqual(shop.GetProduct(product).Price(),NewPrice);
            
        }
        
        [Test]
        public void FindCheapestVariantTest()
        {
            string Product1 = "sugar";
            var Magnit = _shopsService.Create("Magnit");
            var Lenta = _shopsService.Create("Magnit");
            var Pyaterochka = _shopsService.Create("Magnit");
            var sugar = _shopsService.CreateProduct(Product1);
            var salt = new Product("salt");

            Magnit.AddProduct(Product1, 10, 10);
            Lenta.AddProduct(Product1, 12, 10);
            Pyaterochka.AddProduct(Product1, 15, 10);
            
            Assert.AreEqual(_shopsService.FindCheapestVariant(sugar,3), Magnit);
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.FindCheapestVariant(sugar, 100);
            });
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.FindCheapestVariant(salt, 1);
            });
        }
        
        [Test]
        public void BuyBigAmountOfProductsTest()
        {
            int moneyBefore = 200;
            int productPrice = 120;
            int productToBuyCount = 1;
            int productCount = 20;
            string productName = "sugar";
            var customer = new Customer("Ilya", moneyBefore);
            var shop = _shopsService.Create("Magnit");
            var product = _shopsService.CreateProduct(productName);

            shop.AddProduct(productName, productPrice, productCount);
            shop.Buy(customer,productName,productToBuyCount);
            
            Assert.AreEqual(moneyBefore - productPrice  * productToBuyCount, customer.Money());
            Assert.AreEqual(productCount - productToBuyCount , shop.GetProduct(product).Count());
            
        }
    }
}