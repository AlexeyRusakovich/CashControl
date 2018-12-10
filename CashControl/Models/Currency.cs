using System;
using System.Collections.Generic;

namespace CashControl.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string FullName { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
