using AccountingNotebook.BLL.Dtos;
using System.Collections.Generic;
using AccountingNotebook.Enums.Enums;

namespace AccountingNotebook.BLL.Services.Interfaces
{
    public interface ITransactionService
    {
        TransactionDto CommitTransaction(double amount, TransactionType transactionType);
        ICollection<TransactionDto> GetAllTransactions();
        TransactionDto GetTransactionById(string id);
    }
}