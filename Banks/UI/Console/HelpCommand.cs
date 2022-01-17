using Banks.UI.Console.Tools;

namespace Banks.UI.Console
{
    internal class HelpCommand : NonTerminatingCommand
    {
        public HelpCommand(IUserInterface userInterface)
            : base(userInterface)
        {
        }

        protected override bool InternalCommand()
        {
            Interface.WriteMessage(UserInterfaceMessages.HelpMessage);
            return true;
        }
    }
}