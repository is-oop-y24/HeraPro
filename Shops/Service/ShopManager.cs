using System.Collections.Generic;
using System.Linq;
using Shops.Entity;
using Shops.ValueObj;

namespace Shops.Service
{
    public class ShopManager : IShopManager
    {
        private readonly CashStack _cashStack;
        private readonly CustomerStack _customerStack;
        private readonly ProductStack _productStack;
        private readonly ShopStack _shopStack;

        public ShopManager()
        {
            _cashStack = new CashStack();
            _customerStack = new CustomerStack();
            _productStack = new ProductStack();
            _shopStack = new ShopStack();
        }

        public Person CreateCustomer(decimal balance)
        {
            return _customerStack.CreateCustomer(_cashStack.CreateCashAccount(balance));
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _customerStack.GetAllPersons();
        }

        public bool AddProduct(Shop shop, string name, int quantity, decimal price)
        {
            return _productStack.AddProduct(shop, name, quantity, price);
        }

        public bool AddProduct(Shop shop, Product product)
        {
            return _productStack.AddProduct(shop, product);
        }

        public bool AddProduct(Shop shop, IEnumerable<Product> products)
        {
            return _productStack.AddProduct(shop, products);
        }

        public Product BuyProductFromShop(Product product, int quantity, Shop shop, Person customer)
        {
            if (quantity <= 0)
                return null;
            if (quantity > product.Quantity)
                return null;

            bool successPay =
                _cashStack.TransferFromTo(customer.CashAccount, shop.CashAccount, product.Price * quantity);
            if (!successPay)
                return null;

            if (_productStack.DeleteProduct(shop, product))
            {
                var newProduct = new Product(product.Name, product.Id, product.Quantity - quantity, product.Price);
                var productToReturn = new Product(product.Name, product.Id, quantity, product.Price);

                _productStack.AddProduct(shop, newProduct);
                return productToReturn;
            }

            _cashStack.TransferFromTo(shop.CashAccount, customer.CashAccount, product.Price * quantity);
            return null;
        }

        public IEnumerable<Product> BuyProductFromShop(IEnumerable<Product> products, Shop shop, Person customer)
        {
            return products.Select(x => BuyProductFromShop(x, x.Quantity, shop, customer));
        }

        public Product FindProductByName(Shop shop, string name)
        {
            return _productStack.FindProductByName(shop, name);
        }

        public Product FindProductById(Shop shop, int id)
        {
            return _productStack.FindProductById(shop, id);
        }

        public Product FindProductByName(string name)
        {
            return GetAllShops()
                .Select(x => _productStack.FindProductByName(x, name)).FirstOrDefault(tmpProduct => tmpProduct != null);
        }

        public Product FindProductById(int id)
        {
            return GetAllShops()
                .Select(x => _productStack.FindProductById(x, id)).FirstOrDefault(tmpProduct => tmpProduct != null);
        }

        public bool ChangeCostOfProduct(Shop shop, Product product, decimal price)
        {
            return _productStack.ChangeCostOfProduct(shop, product, price);
        }

        public IEnumerable<KeyValuePair<int, List<Product>>> GetAllProducts()
        {
            return _productStack.GetAllProducts();
        }

        public IEnumerable<Product> GetAllProducts(Shop shop)
        {
            return _productStack.GetAllProducts(shop);
        }

        public Shop CreateShop(string name, string address, int balance)
        {
            return _shopStack.CreateShop(name, address, _cashStack.CreateCashAccount(balance));
        }

        public Shop FindShopByName(string name)
        {
            return _shopStack.FindShopByName(name);
        }

        public Shop FindShopByAddress(string address)
        {
            return _shopStack.FindShopByAddress(address);
        }

        public Shop FindShopById(int id)
        {
            return _shopStack.FindShopById(id);
        }

        public Shop FindTheCheapestShopForTheseProducts(IEnumerable<Product> products)
        {
            var tmpProducts = products.ToList();
            if (tmpProducts.Count == 0) return null;

            decimal price = decimal.MaxValue;
            object shopToReturn = null;
            foreach (Shop shop in GetAllShops())
            {
                decimal tmpPrice = 0;
                var tmpShopProducts = GetAllProducts(shop).ToList();
                if (tmpShopProducts.Count < tmpProducts.Count) continue;

                foreach (Product product in tmpProducts)
                {
                    int index = tmpShopProducts.FindIndex(x => x.Name.Equals(product.Name));
                    if (index == -1)
                    {
                        tmpPrice = decimal.MaxValue;
                        break;
                    }

                    if (tmpProducts[index].Quantity < product.Quantity)
                    {
                        tmpPrice = decimal.MaxValue;
                        break;
                    }

                    tmpPrice += product.Price;
                }

                if (price < tmpPrice) continue;
                shopToReturn = shop;
                price = tmpPrice;
            }

            return (Shop)shopToReturn;
        }

        public IEnumerable<Shop> GetAllShops()
        {
            return _shopStack.GetAllShops();
        }

        public decimal ShowCashInAccount(Shop account)
        {
            return _cashStack.ShowCashInAccount(account.CashAccount);
        }

        public decimal ShowCashInAccount(Person account)
        {
            return _cashStack.ShowCashInAccount(account.CashAccount);
        }
    }
}