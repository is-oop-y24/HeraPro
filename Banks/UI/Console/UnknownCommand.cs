namespace Banks.UI.Console
{
    internal class UnknownCommand : NonTerminatingCommand
    {
        public UnknownCommand(IUserInterface userInterface)
            : base(userInterface)
        {
        }

        protected override bool InternalCommand()
        {
            Interface.WriteMessage("Unknown command");
            return false;
        }
    }
}