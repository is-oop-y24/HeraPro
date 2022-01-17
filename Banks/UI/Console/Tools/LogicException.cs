using System;

namespace Banks.UI.Console.Tools
{
    public class LogicException : Exception
    {
        public const string NegativeNumber = "Value should be positive number";
        public const string PositiveNumber = "Value should be negative number";
        public const string NotPercent = "Value should be percent";

        public const string BankAlreadyExists = "Bank already exists";
        public const string NoSuchBank = "No such bank";

        public const string NoSuchBankAccountType = "No such bank account type";

        public const string WrongPassportFormat = "Wrong passport format";

        public const string CreditAccountLimitReached = "Credit account limit reached";

        public const string BalanceCantBeNegative = "Balance can't be negative for this account";

        public const string CantWithdrawFromDepositAccountYet = "Can't withdraw from deposit account yet";

        public const string ExceededSuspiciousAccountLimit = "Exceeded suspicious account limit";

        public const string NoSuchTransactionType = "No such transaction type";

        public const string NoTransactionWithSuchId = "No transaction with such id";

        public const string NoBankAccountWithSuchId = "No bank account with such id";

        public const string NewDateShouldComeAfterCurrentDate = "New date should come after current date";

        public const string IncorrectDateFormat = "Incorrect date format";
        public LogicException(string message)
            : base(message)
        {
        }
    }
}