namespace Banks.UI.Console.Services
{
    public class BanksService : IBanksService
    {
        private readonly IUserInterface _userInterface;
        private readonly IUserCommandFactory _commandFactory;

        public BanksService(IUserInterface userInterface, IUserCommandFactory commandFactory)
        {
            _userInterface = userInterface;
            _commandFactory = commandFactory;
        }

        public void Run()
        {
            (bool wasSuccessful, bool shouldQuit) response = _commandFactory.GetCommand("?").RunCommand();
            while (!response.shouldQuit)
            {
                string input = _userInterface.ReadValue("> ");
                UserCommand command = _commandFactory.GetCommand(input);

                response = command.RunCommand();

                if (!response.wasSuccessful)
                {
                    _userInterface.WriteMessage("Enter ? to view options");
                }
            }
        }
    }
}