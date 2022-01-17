using System.Collections.Generic;
using System.Linq;
using Banks.BankService.Accounts;
using Banks.BankService.Accounts.CreditAccount;
using Banks.BankService.Accounts.DebitAccount;
using Banks.BankService.Accounts.DepositAccount;
using Banks.BankService.Banks;
using Banks.BankService.Clients;

namespace Banks.UI.Console
{
    internal class GetBankAccountsCommand : NonTerminatingCommand
    {
        private readonly CentralBank _centralBank;
        public GetBankAccountsCommand(IUserInterface userInterface, CentralBank centralBank)
            : base(userInterface)
        {
            _centralBank = centralBank;
        }

        protected override bool InternalCommand()
        {
            foreach (IBank bank in _centralBank.GetBanksList())
            {
                Interface.WriteMessage($"{bank.Name} accounts:");
                foreach (IClient client in bank.GetClientsList())
                {
                    foreach (Account account in client.Accounts)
                    {
                        string output = $"\tClient name: {client.Name}\n" +
                                        $"\tClient surname: {client.Surname}\n" +
                                        $"\tBalance: {account.Balance}\n";
                        if (account is ICreditAccount creditAccount)
                        {
                            output += $"\tCurrent commission: {creditAccount.Commission}\n" +
                                      $"\tCurrent limit: {creditAccount.Limit}";
                        }
                        else if (account is IDebitAccount debitAccount)
                        {
                            output += $"\tCurrent balance payment: {debitAccount.BalancePayment}\n";
                        }
                        else if (account is IDepositAccount depositAccount)
                        {
                            List<DepositCommission> fee = depositAccount.PercentByBalance;
                            fee.Sort();
                            double percent = 0;
                            foreach (DepositCommission commission in fee.Where(commission => account.Balance <= commission.Price))
                            {
                                percent = commission.Percent;
                            }

                            output += $"\tStart period: {depositAccount.StartPeriod}\n" +
                                      $"\tEnd period: {depositAccount.EndPeriod}" +
                                      $"\tPercent: {percent}";
                        }

                        output += "\n";

                        Interface.WriteMessage(output);
                    }
                }
            }

            return true;
        }
    }
}