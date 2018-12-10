using System;
using System.Collections.Generic;

namespace CashControl.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string UserLogin { get; set; }
        public int TransactionTypeId { get; set; }
        public int CurrencyId { get; set; }
        public string Categories { get; set; }

        public Currency Currency { get; set; }
        public TransactionType TransactionType { get; set; }
        public Users UserLoginNavigation { get; set; }
    }
}
