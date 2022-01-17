namespace Banks.UI.Console
{
    public abstract class UserCommand
    {
        private readonly bool _isTerminatingCommand;
        protected UserCommand(bool isTerminatingCommand, IUserInterface userInterface)
        {
            _isTerminatingCommand = isTerminatingCommand;
            Interface = userInterface;
        }

        protected IUserInterface Interface { get; }

        public (bool wasSuccessful, bool shouldQuit) RunCommand()
        {
            if (this is IParameterisedCommand parameterisedCommand)
            {
                bool allParametersCompleted = false;
                while (allParametersCompleted == false)
                {
                    allParametersCompleted = parameterisedCommand.GetParameters();
                }
            }

            return (InternalCommand(), _isTerminatingCommand);
        }

        internal string GetParameter(string parameterName)
        {
            return Interface.ReadValue($"Enter {parameterName}:");
        }

        protected abstract bool InternalCommand();
    }
}