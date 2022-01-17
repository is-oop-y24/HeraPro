using System.Collections.Generic;
using Banks.BankService.Accounts;

namespace Banks.BankService.Clients
{
    public interface IClient
    {
        string Name { get; }
        string Surname { get; }
        string Address { get; }
        string Passport { get; }

        List<Account> Accounts { get; }
    }
}