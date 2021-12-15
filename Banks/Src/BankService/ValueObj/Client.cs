using System.Collections.Generic;

namespace Banks.BankService.ValueObj
{
    public class Client
    {
        internal Client(string name)
        {
            Name = name;
            IdAccounts = new List<int>();
            Trusted = false;
        }

        internal Client(string name, string address)
        {
            Name = name;
            Address = address;
            IdAccounts = new List<int>();
            Trusted = false;
        }

        internal Client(string name, int passport)
        {
            Name = name;
            Passport = passport;
            IdAccounts = new List<int>();
            Trusted = false;
        }

        internal Client(string name, string address, int passport)
        {
            Name = name;
            Address = address;
            Passport = passport;
            IdAccounts = new List<int>();
            Trusted = true;
        }

        internal Client(Client client, int idAccount)
        {
            Name = client.Name;
            Address = client.Address;
            Passport = client.Passport;
            IdAccounts = client.IdAccounts;
            IdAccounts.Add(idAccount);
            Trusted = client.Trusted;
        }

        public string Name { get; }
        public string Address { get; }
        public int Passport { get; }
        public bool Trusted { get; }
        public List<int> IdAccounts { get; }
    }
}