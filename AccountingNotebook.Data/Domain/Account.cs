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
        private readonly ICollection<Transaction> _transactions;

        private static readonly Account Instance = new Account();

        private readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        private Account()
        {
            Balance = 0;
            _transactions = new List<Transaction>();
        }

        public ICollection<Transaction> Transactions
        {
            get
            {
                try
                {
                    _locker.EnterReadLock();
                    return _transactions;
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public static Account GetInstance()
        {
            return Instance;
        }

        public void Debit(double amount, string transactionId)
        {
            try
            {
                _locker.EnterWriteLock();
                var transaction = _transactions
                    .FirstOrDefault(t => t.Id == transactionId);

                if (transaction == null)
                {
                    throw new ArgumentException();
                }

                Balance += amount;
                transaction.TransactionStatus = TransactionStatus.Success;
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void Credit(double amount, string transactionId)
        {
            try
            {
                _locker.EnterWriteLock();
                var transaction = _transactions
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
            finally
            {
                _locker.ExitWriteLock();
            }

        }
    }
}