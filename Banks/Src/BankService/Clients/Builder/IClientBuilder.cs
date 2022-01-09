namespace Banks.BankService.Clients.Builder
{
    public interface IClientBuilder
    {
        void BuildName(string name);
        void BuildSurname(string surname);
        void BuildAddress(string address);
        void BuildPassport(string passport);
    }
}