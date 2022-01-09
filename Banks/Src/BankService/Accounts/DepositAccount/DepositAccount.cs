using System;
using System.Collections.Generic;

namespace Banks.BankService.Accounts.DepositAccount
{
    public class DepositAccount : Account, IDepositAccount
    {
        public DepositAccount(double balance, DateTime startPeriod, DateTime endPeriod, List<DepositCommission> percentByBalance)
        : base(balance)
        {
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            PercentByBalance = percentByBalance;
        }

        public List<DepositCommission> PercentByBalance { get; }
        public DateTime StartPeriod { get; }
        public DateTime EndPeriod { get; }
        public double Cashback { get; private set; }

        internal override bool Withdraw(double money)
        {
            if (DateTime.Now < EndPeriod) return false;

            if (money < 0) return false;

            double diff = Balance - money;
            if (diff < 0) return false;

            Balance = diff;
            return true;
        }

        internal override bool Deposit(double money)
        {
            if (money < 0) return false;

            Balance += money;
            return true;
        }

        internal override bool DoCommission()
        {
            double result = CheckCommission(1);
            if (result == 0) return false;
            Cashback += result;
            return true;
        }

        internal override double CheckCommission(int days)
        {
            if (days < 0) return 0;

            PercentByBalance.Sort();
            DepositCommission fee = PercentByBalance.Find(x => x.Price > Balance);
            int count = (EndPeriod - StartPeriod).Days - days;
            if (fee == null) return 0;
            return Balance * (fee.Percent / count);
        }
    }
}