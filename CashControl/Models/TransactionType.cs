using System;
using System.Collections.Generic;

namespace CashControl.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
