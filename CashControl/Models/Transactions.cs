using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashControl.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public decimal Amount { get; set; }
        public string UserLogin { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public string Categories { get; set; }

        public Currency Currency { get; set; }
        public TransactionType TransactionType { get; set; }
        public Users UserLoginNavigation { get; set; }
    }
}
