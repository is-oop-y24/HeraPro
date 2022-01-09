namespace Banks.BankService.Clients.Builder
{
    public class ClientBuilder : IClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;

        public ClientBuilder()
        {
            Reset();
        }

        public void BuildName(string name)
        {
            _name = name;
        }

        public void BuildSurname(string surname)
        {
            _surname = surname;
        }

        public void BuildAddress(string address)
        {
            _address = address;
        }

        public void BuildPassport(string passport)
        {
            _passport = passport;
        }

        public IClient GetClient()
        {
            if (_name == null || _surname == null)
                return null;

            var result = new Client(_name, _surname, _address, _passport);
            Reset();
            return result;
        }

        private void Reset()
        {
            _name = null;
            _surname = null;
            _address = null;
            _passport = null;
        }
    }
}