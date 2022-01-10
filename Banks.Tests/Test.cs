using System;
using System.Diagnostics;
using System.Linq;
using Banks.BankService.Accounts.BankAccount;
using Banks.BankService.Accounts.CreditAccount;
using Banks.BankService.Accounts.DebitAccount;
using Banks.BankService.Accounts.DepositAccount;
using Banks.BankService.Banks;
using Banks.BankService.Clients;
using Banks.BankService.Clients.Builder;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Test
    {
        private CentralBank _cb;

        private string _nameCT;
        private string _surnameCT;
        private string _passportCT;
        private string _addressCT;

        private string _nameCT1;
        private string _surnameCT1;
        private string _passportCT1;
        private string _addressCT1;

        private string _nameBK;
        private string _nameBK1;


        [SetUp]
        public void Setup()
        {
            _cb = new CentralBank();
            _nameCT = "Ivan";
            _surnameCT = "Ivanov";
            _passportCT = "12345";
            _addressCT = "Krasnaya 56";
            _nameBK = "SomeBank";
            _nameBK1 = "Somebank1";
            _nameCT1 = "Peter";
            _surnameCT1 = "Green";
            _passportCT1 = "54321";
            _addressCT1 = "Nevskiy 2";
        }

        [Test]
        public void CreateClientAndAccounts()
        {
            var bank = _cb.CreateDefaultBank(_nameBK) as Bank;
            if (bank == null) return;
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCT);
            clientBuilder.BuildSurname(_surnameCT);
            clientBuilder.BuildPassport(_passportCT);
            clientBuilder.BuildAddress(_addressCT);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCT1);
            clientBuilder.BuildSurname(_surnameCT1);
            clientBuilder.BuildPassport(_passportCT1);
            clientBuilder.BuildAddress(_addressCT1);
            IClient client1 = bank.AddClient();
            CreditAccount credit = bank.AddCreditAccount(client as Client, 100);
            DebitAccount debit = bank.AddDebitAccount(client1 as Client, 200);
            DepositAccount deposit = bank.AddDepositAccount(client as Client, 300, DateTime.Today.AddDays(2));
            Assert.AreSame(credit, client.Accounts.First());
            Assert.AreSame(debit, client1.Accounts.First());
            Assert.AreEqual(2, client.Accounts.Count);
        }

        [Test]
        public void Transfer()
        {
            var bank = _cb.CreateDefaultBank(_nameBK) as Bank;
            var bank1 = _cb.CreateDefaultBank(_nameBK1) as Bank;
            IClientBuilder clientBuilder = bank!.CreateClientBuilder();
            IClientBuilder clientBuilder1 = bank1!.CreateClientBuilder();
            clientBuilder.BuildName(_nameCT);
            clientBuilder.BuildSurname(_surnameCT);
            clientBuilder.BuildPassport(_passportCT);
            clientBuilder.BuildAddress(_addressCT);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCT1);
            clientBuilder.BuildSurname(_surnameCT1);
            clientBuilder.BuildPassport(_passportCT1);
            clientBuilder.BuildAddress(_addressCT1);
            IClient client1 = bank.AddClient();
            clientBuilder1.BuildName(_nameCT);
            clientBuilder1.BuildSurname(_surnameCT);
            clientBuilder1.BuildPassport(_passportCT);
            clientBuilder1.BuildAddress(_addressCT);
            IClient clientFromBank1 = bank1.AddClient();
            CreditAccount credit = bank1.AddCreditAccount(clientFromBank1 as Client, 100);
            DebitAccount debit = bank.AddDebitAccount(client1 as Client, 200);
            DepositAccount deposit = bank.AddDepositAccount(client as Client, 300, DateTime.Today.AddDays(2));

            Assert.IsFalse(bank.TransferFromTo(client1, debit, client, deposit, 400));

            bank.TransferFromTo(client1, debit, client, deposit, 200);
            Assert.AreEqual(0, debit.Balance);
            Assert.AreEqual(500, deposit.Balance);
            Assert.False(bank.TransferFromTo(clientFromBank1, credit, client, debit, 150));
            Assert.False(bank.TransferFromTo(clientFromBank1, deposit, client, debit, 150));
            Assert.False(bank.TransferFromTo(client, deposit, client1, debit, 150));

            CreditAccount credit1 = bank.AddCreditAccount(client1 as Client, 500);
            bank.TransferFromTo(client1, credit1, client1, debit, 1000);
            Assert.AreEqual(-600, credit1.Balance);
        }

        [Test]
        public void Subscribe()
        {
            var bank = _cb.CreateDefaultBank(_nameBK) as Bank;
            Debug.Assert(bank != null, nameof(bank) + " != null");
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCT);
            clientBuilder.BuildSurname(_surnameCT);
            clientBuilder.BuildPassport(_passportCT);
            clientBuilder.BuildAddress(_addressCT);
            var client = bank.AddClient() as Client;
            bank.Subscribe(client);
            IBankAccount account = client!.Subject;
            bank.ChangeCommissionForCreditAccount(0.15);
            IBankAccount newAccount = client.Subject;
            Assert.AreNotEqual(account.CommissionForCreditAccount, newAccount.CommissionForCreditAccount);
            bank.Unsubscribe(client);
            bank.ChangeLimitForCreditAccount(3000);
            Assert.AreNotEqual(newAccount.LimitForCreditAccount, bank.Account.LimitForCreditAccount);
        }

        [Test]
        public void RollBackTransaction()
        {
            var bank = _cb.CreateDefaultBank(_nameBK) as Bank;
            if (bank == null) return;
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCT);
            clientBuilder.BuildSurname(_surnameCT);
            clientBuilder.BuildPassport(_passportCT);
            clientBuilder.BuildAddress(_addressCT);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCT1);
            clientBuilder.BuildSurname(_surnameCT1);
            clientBuilder.BuildPassport(_passportCT1);
            clientBuilder.BuildAddress(_addressCT1);
            IClient client1 = bank.AddClient();
            CreditAccount credit = bank.AddCreditAccount(client as Client, 100);
            DebitAccount debit = bank.AddDebitAccount(client1 as Client, 200);
            bank.TransferFromTo(client, credit, client1, debit, 100);
            Assert.AreEqual(300, debit.Balance);
            bank.RollBackTransaction(bank.Operations.First());
            Assert.AreEqual(200, debit.Balance);
        }

        [Test]
        public void CashbackAndPayment()
        {
            var bank = _cb.CreateDefaultBank(_nameBK) as Bank;
            IClientBuilder clientBuilder = bank!.CreateClientBuilder();
            clientBuilder.BuildName(_nameCT);
            clientBuilder.BuildSurname(_surnameCT);
            clientBuilder.BuildPassport(_passportCT);
            clientBuilder.BuildAddress(_addressCT);
            var client = bank.AddClient() as Client;
            CreditAccount credit = bank.AddCreditAccount(client, 100);
            DebitAccount debit = bank.AddDebitAccount(client, 200);
            DepositAccount deposit = bank.AddDepositAccount(client, 300, DateTime.Now.AddSeconds(1));
            _cb.DoCommission();
            _cb.DoPayment();
            Assert.AreEqual(100, credit.Balance);
            Assert.AreEqual(202, debit.Balance);
            Assert.AreEqual(390, deposit.Balance);

        }
    }
}
