using System;
using AccountingNotebook.Enums.Enums;

namespace AccountingNotebook.Enums
{
    public static class EnumParser
    {
        public static TransactionType ParseToTransactionType(string transactionType)
        {
            return (TransactionType)Enum.Parse(typeof(TransactionType), transactionType);
        }
    }
}