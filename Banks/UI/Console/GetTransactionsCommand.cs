using Banks.BankLogService;
using Banks.BankService.Banks;

namespace Banks.UI.Console
{
    internal class GetTransactionsCommand : NonTerminatingCommand
    {
        private readonly CentralBank _centralBank;
        public GetTransactionsCommand(IUserInterface userInterface, CentralBank centralBank)
            : base(userInterface)
        {
            _centralBank = centralBank;
        }

        protected override bool InternalCommand()
        {
            foreach (Log transaction in _centralBank.GetTransactionsByAllBanks())
            {
                Interface.WriteMessage($"\tSender: {transaction.AccountFrom}\n" +
                                       $"\tReceiver: {transaction.AccountTo}\n" +
                                       $"\tType: {transaction.Operation}\n" +
                                       $"\tMoney: {transaction.Money}\n" +
                                       $"\n");
            }

            return true;
        }
    }
}