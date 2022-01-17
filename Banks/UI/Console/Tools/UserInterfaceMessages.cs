namespace Banks.UI.Console.Tools
{
    internal static class UserInterfaceMessages
    {
        public const string QuitMessage = "Quitting now..";

        public const string HelpMessage = "USAGE:\n" +
                                          "\tregisterbank (rb)\n" +
                                          "\topenaccount (oa)\n" +
                                          "\tgetaccounts (ga)\n" +
                                          "\ttransaction (t)\n" +
                                          "\tgettransactions (gt)\n" +
                                          "\treversetransaction (rt)\n" +
                                          "\tfastforward (f)\n" +
                                          "\tupdatebank (ub)\n" +
                                          "\tquit (q)\n" +
                                          "\t?\n" +
                                          "EXAMPLES:\n" +
                                          "\tNew Bank\n" +
                                          "> registerbank\n" +
                                          "\tEnter name: Tinkoff\n" +
                                          "\tEnter Credit account commission amount: 5\n" +
                                          "\tEnter Credit account limit: 100000" +
                                          "\tEnter Debit account interest rate: 3\n" +
                                          "\tEnter Deposit account interest rate before 50k: 3\n" +
                                          "\tEnter Deposit account interest rate before 100k: 3.5\n" +
                                          "\tEnter Deposit account interest rate after 100k: 4\n" +
                                          "\tEnter Suspicious account transaction limit: 5000\n" +
                                          "\tEnter Deposit account time in months: 20\n" +
                                          "\t" + BankRegisteredSuccessfullyMessage + "\n" +
                                          "\n" +
                                          "> openaccount\n" +
                                          "\tEnter bank name: Tinkoff\n" +
                                          "\tEnter type of bank account: debit\n" +
                                          "\tEnter name: Samat\n" +
                                          "\tEnter surname Gainutdinov:\n" +
                                          "\tEnter address: Belorusskaya, 6\n" +
                                          "\tEnter passport: 1111 111111\n" +
                                          "\t" + BankAccountCreatedSuccessfullyMessage + "\n" +
                                          "\n" +
                                          "> transaction\n" +
                                          "\tEnter transaction value: 1000\n" +
                                          "\tEnter transaction type: transfer\n" +
                                          "\tEnter from bank account id: 0\n" +
                                          "\tEnter to bank account id: 1\n" +
                                          "\t" + TransactionCompletedSuccessfullyMessage + "\n" +
                                          "\n" +
                                          "> fastforward\n" +
                                          "\tEnter new date: 2021-12-01\n" +
                                          "\t" + FastForwardTimeCompleted + "\n";

        public const string BankRegisteredSuccessfullyMessage = "Bank registered successfully";
        public const string BankAccountCreatedSuccessfullyMessage = "Bank account created successfully";
        public const string TransactionCompletedSuccessfullyMessage = "Transaction completed successfully";
        public const string FastForwardTimeCompleted = "Fast forward time completed";
        public const string TransactionReversedSuccessfully = "Transaction reversed successfully";
        public const string BankConditionsUpdatedSuccessfully = "Bank conditions updated successfully";
    }
}