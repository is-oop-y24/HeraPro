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

        private string _nameCt;
        private string _surnameCt;
        private string _passportCt;
        private string _addressCt;

        private string _nameCt1;
        private string _surnameCt1;
        private string _passportCt1;
        private string _addressCt1;

        private string _nameBk;
        private string _nameBk1;


        [SetUp]
        public void Setup()
        {
            _cb = new CentralBank();
            _nameCt = "Ivan";
            _surnameCt = "Ivanov";
            _passportCt = "12345";
            _addressCt = "Krasnaya 56";
            _nameBk = "SomeBank";
            _nameBk1 = "Somebank1";
            _nameCt1 = "Peter";
            _surnameCt1 = "Green";
            _passportCt1 = "54321";
            _addressCt1 = "Nevskiy 2";
        }

        [Test]
        public void CreateClientAndAccounts()
        {
            var bank = _cb.CreateDefaultBank(_nameBk) as Bank;
            if (bank == null) return;
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCt);
            clientBuilder.BuildSurname(_surnameCt);
            clientBuilder.BuildPassport(_passportCt);
            clientBuilder.BuildAddress(_addressCt);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCt1);
            clientBuilder.BuildSurname(_surnameCt1);
            clientBuilder.BuildPassport(_passportCt1);
            clientBuilder.BuildAddress(_addressCt1);
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
            var bank = _cb.CreateDefaultBank(_nameBk) as Bank;
            var bank1 = _cb.CreateDefaultBank(_nameBk1) as Bank;
            IClientBuilder clientBuilder = bank!.CreateClientBuilder();
            IClientBuilder clientBuilder1 = bank1!.CreateClientBuilder();
            clientBuilder.BuildName(_nameCt);
            clientBuilder.BuildSurname(_surnameCt);
            clientBuilder.BuildPassport(_passportCt);
            clientBuilder.BuildAddress(_addressCt);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCt1);
            clientBuilder.BuildSurname(_surnameCt1);
            clientBuilder.BuildPassport(_passportCt1);
            clientBuilder.BuildAddress(_addressCt1);
            IClient client1 = bank.AddClient();
            clientBuilder1.BuildName(_nameCt);
            clientBuilder1.BuildSurname(_surnameCt);
            clientBuilder1.BuildPassport(_passportCt);
            clientBuilder1.BuildAddress(_addressCt);
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
            var bank = _cb.CreateDefaultBank(_nameBk) as Bank;
            Debug.Assert(bank != null, nameof(bank) + " != null");
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCt);
            clientBuilder.BuildSurname(_surnameCt);
            clientBuilder.BuildPassport(_passportCt);
            clientBuilder.BuildAddress(_addressCt);
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
            var bank = _cb.CreateDefaultBank(_nameBk) as Bank;
            if (bank == null) return;
            IClientBuilder clientBuilder = bank.CreateClientBuilder();
            clientBuilder.BuildName(_nameCt);
            clientBuilder.BuildSurname(_surnameCt);
            clientBuilder.BuildPassport(_passportCt);
            clientBuilder.BuildAddress(_addressCt);
            IClient client = bank.AddClient();
            clientBuilder.BuildName(_nameCt1);
            clientBuilder.BuildSurname(_surnameCt1);
            clientBuilder.BuildPassport(_passportCt1);
            clientBuilder.BuildAddress(_addressCt1);
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
            var bank = _cb.CreateDefaultBank(_nameBk) as Bank;
            IClientBuilder clientBuilder = bank!.CreateClientBuilder();
            clientBuilder.BuildName(_nameCt);
            clientBuilder.BuildSurname(_surnameCt);
            clientBuilder.BuildPassport(_passportCt);
            clientBuilder.BuildAddress(_addressCt);
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
