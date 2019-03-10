using System;
using AccountingNotebook.Data.Domain;

namespace AccountingNotebook.BLL.Dtos
{
    public class TransactionDto : BaseTransactionDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }

        public TransactionDto(Transaction transaction)
        {
            Id = transaction.Id;
            TransactionType = transaction.TransactionType.ToString();
            Amount = transaction.Amount;
            Date = transaction.Date;
        }
    }
}