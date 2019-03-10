using AccountingNotebook.BLL.Dtos;
using AccountingNotebook.BLL.Services.Interfaces;
using AccountingNotebook.Data.Domain;
using AccountingNotebook.Enums.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AccountingNotebook.BLL.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly Account _account;

        public TransactionService()
        {
            _account = Account.GetInstance();
        }

        #region CommitTransaction

        public TransactionDto CommitTransaction(double amount, TransactionType transactionType)
        {
            Account.IsLocked = true;
            var transaction = new Transaction(amount, transactionType);
            _account.Transactions.Add(transaction);

            switch (transactionType)
            {
                case TransactionType.Credit:
                    _account.Credit(amount, transaction.Id);
                    break;

                case TransactionType.Debit:
                    _account.Debit(amount, transaction.Id);
                    break;
            }

            Account.IsLocked = false;
            return new TransactionDto(transaction);
        }

        #endregion

        #region GetAllTransactions

        public ICollection<TransactionDto> GetAllTransactions()
        {
            return _account.Transactions
                .Where(t => t.TransactionStatus == TransactionStatus.Success)
                .Select(t => new TransactionDto(t)).ToList();
        }

        #endregion

        #region GetTransactionById

        public TransactionDto GetTransactionById(string id)
        {
            var transaction = _account.Transactions
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found");
            }

            return new TransactionDto(transaction);
        }

        #endregion

    }
}