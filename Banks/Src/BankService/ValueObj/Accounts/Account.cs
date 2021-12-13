using System;

namespace Banks.BankService.ValueObj.Accounts
{
    public abstract class Account
    {
        internal Account(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}