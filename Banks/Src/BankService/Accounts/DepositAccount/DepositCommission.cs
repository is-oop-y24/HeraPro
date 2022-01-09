using System;

namespace Banks.BankService.Accounts.DepositAccount
{
    public class DepositCommission : IComparable<DepositCommission>
    {
        public DepositCommission(double percent, double price)
        {
            Percent = percent;
            Price = price;
        }

        public double Percent { get; }
        public double Price { get; }

        public static bool operator <(DepositCommission dp1, DepositCommission dp2)
        {
            return dp1.Price < dp2.Price;
        }

        public static bool operator >(DepositCommission dp1, DepositCommission dp2)
        {
            return dp1.Price > dp2.Price;
        }

        public int CompareTo(DepositCommission other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Price.CompareTo(other.Price);
        }
    }
}