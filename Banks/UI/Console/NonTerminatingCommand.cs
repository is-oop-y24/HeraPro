namespace Banks.UI.Console
{
    internal abstract class NonTerminatingCommand : UserCommand
    {
        protected NonTerminatingCommand(IUserInterface userInterface)
            : base(false, userInterface)
        {
        }
    }
}