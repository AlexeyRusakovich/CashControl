using System;
using System.Collections.Generic;

namespace CashControl.Models
{
    public partial class Users
    {
        public Users()
        {
            Transactions = new HashSet<Transactions>();
        }

        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
