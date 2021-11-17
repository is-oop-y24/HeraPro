using System.Collections.Generic;
using NUnit.Framework;
using Shops.Service;
using Shops.ValueObj;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        [SetUp]
        public void Setup()
        {
            _shopManager = new();
        }

        [Test]
        public void AddProducts()
        {
            Shop shop = _shopManager.CreateShop("shop", "griboedova", 23000);
            Assert.True(_shopManager.AddProduct(shop, "pizza", 4, 200));
            Product product = _shopManager.FindProductByName(shop, "pizza");
            Assert.AreEqual(null, _shopManager.FindProductByName(shop, "pelmeni"));
        }

        [Test]
        public void ChangeCostOfProduct()
        {
            Shop shop = _shopManager.CreateShop("shop", "griboedova", 23000);
            Assert.True(_shopManager.AddProduct(shop, "pizza", 4, 200));
            Assert.True(_shopManager.ChangeCostOfProduct(shop, _shopManager.FindProductByName(shop, "pizza"), 500));
        }
        
        [Test]
        public void FindTheCheapestProducts()
        {
            Shop shop1 = _shopManager.CreateShop("shop1", "griboedova", 23000);
            Shop shop2 = _shopManager.CreateShop("shop2", "nevskiy", 2000);

            _shopManager.AddProduct(shop1, "pizza", 4, 500);
            _shopManager.AddProduct(shop1, "mango", 5, 500);
            _shopManager.AddProduct(shop2, "pizza", 1, 1);
            _shopManager.AddProduct(shop2, "mango", 7, 300);
            IEnumerable<Product> products = _shopManager.GetAllProducts(shop2);
            Assert.AreEqual(shop2, _shopManager.FindTheCheapestShopForTheseProducts(products));
        }
        
        [Test]
        public void BuyProducts()
        {
            Shop shop = _shopManager.CreateShop("shop", "griboedova", 23000);
            _shopManager.AddProduct(shop, "pizza", 4, 500);
            _shopManager.AddProduct(shop, "mango", 5, 500);
            _shopManager.AddProduct(shop, "devil", 6, 550);
            _shopManager.AddProduct(shop, "chilli", 7, 300);
            Person customer = _shopManager.CreateCustomer(1000);
            Product product = _shopManager.BuyProductFromShop(_shopManager.FindProductByName(shop, "chilli"), 2, shop, customer);
            Assert.AreEqual(5, _shopManager.FindProductByName(shop, "chilli").Quantity);
            Assert.AreEqual(2, product.Quantity);
        }
    }
}