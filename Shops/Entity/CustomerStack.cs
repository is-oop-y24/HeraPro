using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    internal class CustomerStack : ICustomerStack
    {
        private readonly List<Person> _listOfPersons;

        internal CustomerStack()
        {
            _listOfPersons = new List<Person>();
        }

        public Person CreateCustomer(CashAccount cashAccount)
        {
            _listOfPersons.Add(new Person(cashAccount));
            return _listOfPersons[^1];
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _listOfPersons.AsReadOnly();
        }
    }
}