using System;
using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    internal class CashStack : ICashStack
    {
        private readonly List<decimal> _balance;

        internal CashStack()
        {
            _balance = new List<decimal>();
        }

        public bool TransferFromTo(CashAccount account1, CashAccount account2, decimal price)
        {
            if (account1 == null || account2 == null)
                return false;

            decimal difference = _balance[account1.Id] - price;
            if (difference < 0)
                return false;

            _balance[account1.Id] = difference;
            _balance[account2.Id] += price;
            return true;
        }

        public CashAccount CreateCashAccount(decimal balance)
        {
            if (balance < 0)
                throw new Exception("Balance cannot be negative");

            var account = new CashAccount(_balance.Count);
            _balance.Add(balance);
            return account;
        }

        public decimal ShowCashInAccount(CashAccount account)
        {
            return _balance[account.Id];
        }
    }
}