using AccountingNotebook.Enums.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AccountingNotebook.Data.Domain
{
    public class Account
    {
        public double Balance { get; private set; }
        public ICollection<Transaction> Transactions { get; }

        private static readonly Account Instance = new Account();
        public static bool IsLocked = false;

        private Account()
        {
            Balance = 0;
            Transactions = new List<Transaction>();
        }

        public static Account GetInstance()
        {
            if (!IsLocked) return Instance;

            Thread.Sleep(200);
            return GetInstance();

        }

        public void Debit(double amount, string transactionId)
        {
            var transaction = Transactions
                .FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null)
            {
                throw new ArgumentException();
            }

            Balance += amount;
            transaction.TransactionStatus = TransactionStatus.Success;
        }

        public void Credit(double amount, string transactionId)
        {
            var transaction = Transactions
                .FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null)
            {
                throw new KeyNotFoundException();
            }

            if (amount > Balance)
            {
                transaction.TransactionStatus = TransactionStatus.Failure;
                throw new ArgumentException("Invalid input");
            }

            Balance -= amount;
            transaction.TransactionStatus = TransactionStatus.Success;
        }

    }
}