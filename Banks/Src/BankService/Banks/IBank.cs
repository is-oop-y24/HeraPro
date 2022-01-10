using System.Collections.Generic;
using Banks.BankLogService;
using Banks.BankService.Accounts.BankAccount;
using Banks.BankService.Clients;

namespace Banks.BankService.Banks
{
    public interface IBank
    {
        string Name { get; }
        BankAccount Account { get; }
        List<Log> Operations { get; }

        void DoCommission();
        void DoPayment();
        IEnumerable<IClient> GetClientsList();
    }
}