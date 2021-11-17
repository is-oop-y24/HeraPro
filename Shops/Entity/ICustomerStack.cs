using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    public interface ICustomerStack
    {
        Person CreateCustomer(CashAccount cashAccount);
        IEnumerable<Person> GetAllPersons();
    }
}