using System.Collections.Generic;
using Banks.BankEventService;
using Banks.BankService.Accounts;
using Banks.BankService.Accounts.BankAccount;

namespace Banks.BankService.Clients
{
    public class Client : IClient, ISubscriber
    {
        public Client(string name, string surname, string address, string passport)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
            Accounts = new List<Account>();
            Subject = null;
        }

        public Client(string name, string surname, string address, string passport, List<Account> accounts, IBankAccount subject)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
            Accounts = accounts;
            Subject = subject;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }
        public List<Account> Accounts { get; }

        public IBankAccount Subject { get; private set; }

        public void Update(IBankAccount subject)
        {
            Subject = subject;
        }
    }
}