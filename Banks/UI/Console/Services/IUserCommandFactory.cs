namespace Banks.UI.Console.Services
{
    public interface IUserCommandFactory
    {
        UserCommand GetCommand(string input);
    }
}