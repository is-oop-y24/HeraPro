using Banks.BankService.Banks;
using Banks.UI.Console.Tools;

namespace Banks.UI.Console
{
    internal class OpenBankAccountCommand : NonTerminatingCommand, IParameterisedCommand
    {
        private readonly CentralBank _centralBank;
        public OpenBankAccountCommand(IUserInterface userInterface, CentralBank centralBank)
            : base(userInterface)
        {
            _centralBank = centralBank;
        }

        internal string BankName { get; private set; }
        internal string ClientName { get; private set; }
        internal string ClientSurname { get; private set; }
        internal string ClientAddress { get; private set; }
        internal string Passport { get; private set; }
        internal string BankAccountType { get; private set; }

        public bool GetParameters()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(BankName))
                {
                    BankName = GetParameter("bank name");
                }

                if (string.IsNullOrWhiteSpace(BankAccountType))
                {
                    BankAccountType = GetParameter("type of bank account");
                }

                if (string.IsNullOrWhiteSpace(ClientName))
                {
                    ClientName = GetParameter("name");
                }

                if (string.IsNullOrWhiteSpace(ClientSurname))
                {
                    ClientSurname = GetParameter("surname");
                }

                if (string.IsNullOrWhiteSpace(ClientAddress))
                {
                    ClientAddress = GetParameter("address");
                }

                if (Passport == null)
                {
                    string input = GetParameter("passport");
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        Passport = input;
                    }
                }

                return !string.IsNullOrWhiteSpace(BankName) && !string.IsNullOrWhiteSpace(BankAccountType)
                                                            && !string.IsNullOrWhiteSpace(ClientName) &&
                                                            !string.IsNullOrWhiteSpace(ClientSurname);
            }
            catch (LogicException e)
            {
                Interface.WriteMessage(e.Message);
                return false;
            }
        }

        protected override bool InternalCommand()
        {
            try
            {
                _centralBank.CreateDefaultBank(BankName);
                Interface.WriteMessage(UserInterfaceMessages.BankAccountCreatedSuccessfullyMessage);
                return true;
            }
            catch (LogicException e)
            {
                Interface.WriteWarning(e.Message);
                return false;
            }
        }
    }
}