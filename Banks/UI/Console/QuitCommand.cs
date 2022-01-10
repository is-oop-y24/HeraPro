using Banks.UI.Console.Tools;

namespace Banks.UI.Console
{
    internal class QuitCommand : UserCommand
    {
        internal QuitCommand(IUserInterface userInterface)
            : base(true, userInterface)
        {
        }

        protected override bool InternalCommand()
        {
            Interface.WriteMessage(UserInterfaceMessages.QuitMessage);
            return true;
        }
    }
}