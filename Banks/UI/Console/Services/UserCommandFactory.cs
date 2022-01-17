using Banks.BankService.Banks;

namespace Banks.UI.Console.Services
{
    internal class UserCommandFactory : IUserCommandFactory
    {
        private readonly IUserInterface _userInterface;
        private readonly CentralBank _centralBank;
        public UserCommandFactory(IUserInterface userInterface, CentralBank centralBank)
        {
            _userInterface = userInterface;
            _centralBank = centralBank;
        }

        public UserCommand GetCommand(string input)
        {
            switch (input.ToLower())
            {
                case "q":
                case "quit":
                    return new QuitCommand(_userInterface);
                case "oa":
                case "openaccount":
                    return new OpenBankAccountCommand(_userInterface, _centralBank);
                case "ga":
                case "getaccounts":
                    return new GetBankAccountsCommand(_userInterface, _centralBank);
                case "gt":
                case "gettransactions":
                    return new GetTransactionsCommand(_userInterface, _centralBank);
                case "?":
                    return new HelpCommand(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }
    }
}