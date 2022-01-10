using Banks.BankService.Banks;
using Banks.UI.Console;
using Banks.UI.Console.Services;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var userInterface = new ConsoleUserInterface();
            var centralBank = new CentralBank();
            var userCommandFactory = new UserCommandFactory(userInterface, centralBank);
            var banksService = new BanksService(userInterface, userCommandFactory);
            banksService.Run();
        }
    }
}
