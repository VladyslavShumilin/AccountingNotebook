using AccountingNotebook.Enums.Enums;
using System;

namespace AccountingNotebook.Data.Domain
{
    public class Transaction
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        public Transaction(double amount, TransactionType transactionType)
        {
            Id = Guid.NewGuid().ToString();
            Amount = amount;
            Date = DateTime.Now;
            TransactionType = transactionType;
            TransactionStatus = TransactionStatus.Processing;
        }
    }
}